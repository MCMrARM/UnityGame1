using Mahou.Config;
using UnityEngine;

namespace Mahou.Combat
{
    class AnimationAwareSpellCaster : SpellCaster
    {

        public Animator targetAnimator;
        private bool animDone = false;

        public override bool BeginCast(SpellConfig spell)
        {
            if (base.BeginCast(spell))
            {
                targetAnimator.SetBool("Attacking", true); // TODO:
                return true;
            }
            return false;
        }

        protected override bool CanFinishCast()
        {
            return HasMinimumCastTimePassed() && animDone;
        }

        protected override void FinishCast()
        {
            base.FinishCast();
            targetAnimator.SetBool("Attacking", false);
            animDone = false;
        }


        public void OnAnimAttackReady(SpellCastLocation forLocation)
        {
            if (HasMinimumCastTimePassed())
                animDone = true;
        }

    }
}
