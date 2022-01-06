using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{

    public class AIState_Wasp_FarAttack : AIState
    {

        public Transform target;
        public SpellConfig spell;
        public KeepWithinRangeConfig rangeKeepConfig;
        public float yawWithin = 5f;

        public override bool Update()
        {
            if (target == null)
                return true;
            KeepWithinRange(target.position, rangeKeepConfig);
            RotateYaw(target.position);
            if (spell.type == SpellType.Projectile && IsYawWithin(target.position, yawWithin))
                BeginCast(spell, target.position);
            return false;
        }

    }

}
