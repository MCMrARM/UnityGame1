using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{

    public class AIState_Wasp_FarAttack : AIState
    {

        public Transform target;
        public SpellConfig spell;
        public KeepWithinRangeConfig rangeKeepConfig;

        public override bool Update()
        {
            if (target == null)
                return true;
            KeepWithinRange(target.position, rangeKeepConfig);
            RotateYaw(target.position);
            if (spell.type == SpellType.Projectile)
                BeginCast(spell, target.position);
            return false;
        }

    }

}
