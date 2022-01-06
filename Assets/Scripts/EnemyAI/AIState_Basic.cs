using UnityEngine;

namespace Mahou.EnemyAI
{

    public class AIState_MoveTowards : AIState
    {

        public Vector3 target;

        public override bool Update()
        {
            return MoveTowards(target);
        }

    }

    public class AIState_RandomWanderV1 : AIState
    {

        public float range;
        public Vector3 target = Vector3.zero;

        public override bool Update()
        {
            if (target == Vector3.zero || !MoveTowards(target))
            {
                PickNewTarget();
            }
            return false;
        }

        private void PickNewTarget()
        {
            target = StateMachine.startPos + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        }

    }


    public class AIState_RandomWander : AIState
    {

        public float range;
        public Vector3 target = Vector3.zero;
        private float cooldownBeforeMove;

        public override bool Update()
        {
            if (cooldownBeforeMove > 0f)
            {
                cooldownBeforeMove -= Time.deltaTime;
                if (cooldownBeforeMove > 0f)
                    return false;
            }

            if (target == Vector3.zero)
                PickNewTarget();
            RotateYaw(target);
            if (IsYawWithin(target, 5f) && !MoveTowards(target))
            {
                PickNewTarget();
            }
            return false;
        }

        private void PickNewTarget()
        {
            do
            {
                target = StateMachine.startPos + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
            }
            while (Vector3.Distance(target, StateMachine.transform.position) < 0.5f);
            cooldownBeforeMove = Random.Range(1f, 5f);
        }

    }


}
