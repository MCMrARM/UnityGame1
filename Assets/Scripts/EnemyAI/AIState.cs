using Mahou.Config;
using System;
using UnityEngine;

namespace Mahou.EnemyAI
{
    [Serializable]
    public struct KeepWithinRangeConfig
    {
        public float minRange, maxRange;
        public float nearingMoveSpeedMul, distancingMoveSpeedMul;
    }

    public abstract class AIState
    {

        public AIStateMachine StateMachine;

        abstract public bool Update();


        private static KeepWithinRangeConfig MoveTowardsConfig = new KeepWithinRangeConfig()
        {
            minRange = 0f,
            maxRange = 0.05f,
            nearingMoveSpeedMul = 1f,
            distancingMoveSpeedMul = 1f
        };

        protected bool MoveTowards(Vector3 pos)
        {
            return KeepWithinRange(pos, MoveTowardsConfig);
        }

        protected bool KeepWithinRange(Vector3 pos, KeepWithinRangeConfig config)
        {
            var t = StateMachine.gameObject.transform;
            pos.y = t.position.y; // TODO:
            float dist = Vector3.Distance(pos, t.position);
            if (dist >= config.minRange && dist <= config.maxRange)
                return false;

            // TODO: pathfinding
            if (dist < config.minRange)
            {
                StateMachine.rigidbody.velocity = -(pos - t.position).normalized * StateMachine.moveSpeed * config.distancingMoveSpeedMul;
            }
            else
            {
                StateMachine.rigidbody.velocity = (pos - t.position).normalized * Math.Min(dist * 10f, 1f) * StateMachine.moveSpeed * config.nearingMoveSpeedMul;
            }

            return true;
        }

        protected bool RotateYaw(Vector3 pos, float turnSpeedMul = 1f)
        {
            var t = StateMachine.gameObject.transform;
            var newAngles = Quaternion.LookRotation(pos - t.position, Vector3.up).eulerAngles;
            var currentAngles = t.rotation.eulerAngles;
            float curRotY = currentAngles.y % 360;
            float targetRotY = (180 + newAngles.y) % 360;
            if (curRotY == targetRotY)
                return false;
            if (targetRotY > curRotY && curRotY - (targetRotY - 360) < targetRotY - curRotY)
                targetRotY -= 360;
            else if (curRotY > targetRotY && targetRotY - (curRotY - 360) < curRotY - targetRotY)
                curRotY -= 360;
            currentAngles.y = Mathf.MoveTowards(curRotY, targetRotY, StateMachine.turnSpeed * turnSpeedMul);
            t.rotation = Quaternion.Euler(currentAngles);
            return true;
        }

        protected bool IsYawWithin(Vector3 pos, float range)
        {
            var t = StateMachine.gameObject.transform;
            var newAngles = Quaternion.LookRotation(pos - t.position, Vector3.up).eulerAngles;
            var currentAngles = t.rotation.eulerAngles;
            float curRotY = currentAngles.y % 360;
            float targetRotY = (180 + newAngles.y) % 360;
            return Math.Min(Mathf.Abs(targetRotY - curRotY), Mathf.Abs(curRotY + 360 - targetRotY)) < range;
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
