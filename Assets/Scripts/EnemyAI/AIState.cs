using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{
    public abstract class AIState
    {

        public AIStateMachine StateMachine;

        abstract public bool Update();

        protected bool MoveTowards(Vector3 pos)
        {
            return true;
        }

        protected bool RotateYaw(Vector3 pos, float turnSpeed)
        {
            var t = StateMachine.gameObject.transform;
            var newAngles = Quaternion.LookRotation(pos - t.position, Vector3.up).eulerAngles;
            var currentAngles = t.rotation.eulerAngles;
            if (currentAngles.y == newAngles.y)
                return false;
            float curRotY = currentAngles.y % 360;
            float targetRotY = (180 + newAngles.y) % 360;
            if (targetRotY > curRotY && curRotY - (targetRotY - 360) < targetRotY - curRotY)
                targetRotY -= 360;
            else if (curRotY > targetRotY && targetRotY - (curRotY - 360) < curRotY - targetRotY)
                curRotY -= 360;
            currentAngles.y = Mathf.MoveTowards(curRotY, targetRotY, turnSpeed);
            t.rotation = Quaternion.Euler(currentAngles);
            return true;
        }

        protected bool CanCast(SpellConfig spell)
        {
            return StateMachine.spellCaster.CanCast(spell);
        }

        protected bool BeginCast(SpellConfig spell, Vector3 target)
        {
            return StateMachine.spellCaster.BeginCast(spell, target);
        }

        protected void CancelCast()
        {
            StateMachine.spellCaster.CancelCast();
        }

    }
}
