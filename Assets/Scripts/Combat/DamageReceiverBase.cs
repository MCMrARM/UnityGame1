using UnityEngine;

namespace Mahou.Combat
{
    public abstract class DamageReceiverBase : MonoBehaviour
    {
        public abstract void OnReceiveDamage(DamageType type, float dmg, StatManager attacker);
    }
}
