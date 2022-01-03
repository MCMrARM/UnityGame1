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

        private void Start()
        {
            _targetSelector = GetComponent<AITargetSelector>();
        }

        protected override void UpdateState(bool lastComplete)
        {
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
