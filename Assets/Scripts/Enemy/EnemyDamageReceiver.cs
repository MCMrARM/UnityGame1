using Mahou.Combat;
using UnityEngine;

namespace Mahou
{

    public class EnemyDamageReceiver : DamageReceiverBase
    {
        public StatManager statManager;

        public override void OnReceiveDamage(DamageType type, float dmg, StatManager attacker)
        {
            var finalDmg = statManager.CalculateDamage(type, dmg, attacker);
            statManager.hp = Mathf.Max(statManager.hp - finalDmg, 0);
        }
    }

}
