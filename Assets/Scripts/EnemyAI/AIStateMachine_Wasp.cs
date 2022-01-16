using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{

    [RequireComponent(typeof(AITargetSelector))]
    public class AIStateMachine_Wasp : AIStateMachine
    {

        private AITargetSelector _targetSelector;
        public SpellConfig attackSpell;
        private bool idle = true;
        public KeepWithinRangeConfig farRangeKeepConfig;
        public float wanderRange;
        public float alertRange;

        protected override void Start()
        {
            base.Start();
            _targetSelector = GetComponent<AITargetSelector>();
        }

        protected override void UpdateState(bool lastComplete)
        {
            if (lastComplete)
            {
                CurrentState = new AIState_RandomWander() { range = wanderRange };
                idle = true;
            }

            if (idle)
            {
                var target = _targetSelector.GetTarget();
                if (target != null)
                {
                    idle = false;
                    _targetSelector.AlertNearby(alertRange, target);
                    CurrentState = new AIState_Wasp_FarAttack() { spell = attackSpell, target = target.transform, rangeKeepConfig = farRangeKeepConfig };
                }
            }
        }

    }

}
