using UnityEngine;

namespace Mahou.Combat
{
    class AnimationAwareSpellCasterEventReceiver : MonoBehaviour
    {

        public AnimationAwareSpellCaster caster;

        public void ANIM_FinishAttack_ProjectilePrimary()
        {
            caster.OnAnimAttackReady(Config.SpellCastLocation.ProjectilePrimary);
        }

    }
}
