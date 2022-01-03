using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{
    public class AIState_Wasp_FarAttack : AIState
    {

        public Transform target;
        public SpellConfig spell;

        public override bool Update()
        {
            RotateYaw(target.position, 1f);
            if (spell.type == SpellType.Projectile)
                BeginCast(spell, target.position);
            return false;
        }

    }

}
