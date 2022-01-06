using Mahou.Combat;
using UnityEngine;

namespace Mahou
{

    public class EnemyDamageReceiver : DamageReceiverBase
    {
        public StatManager statManager;

        public delegate void DamageReceivedAction(DamageType type, float dmg, float finalDmg, StatManager attacker);
        public DamageReceivedAction DamageReceived;

        public override void OnReceiveDamage(DamageType type, float dmg, StatManager attacker)
        {
            var finalDmg = statManager.CalculateDamage(type, dmg, attacker);
            statManager.hp = Mathf.Max(statManager.hp - finalDmg, 0);
            if (DamageReceived != null)
                DamageReceived(type, dmg, finalDmg, attacker);
        }
    }

}
