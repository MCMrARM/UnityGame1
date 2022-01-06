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
                    CurrentState = new AIState_Wasp_FarAttack() { spell = attackSpell, target = target.transform, rangeKeepConfig = farRangeKeepConfig };
                }
            }
        }

    }

}
