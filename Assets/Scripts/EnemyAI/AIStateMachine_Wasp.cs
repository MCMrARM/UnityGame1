using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{
    public class AIStateMachine_Wasp : AIStateMachine
    {

        public SpellConfig attackSpell;

        protected override void UpdateState(bool lastComplete)
        {
            if (lastComplete)
                CurrentState = new AIState_Wasp_FarAttack() { spell = attackSpell, target = GameObject.Find("Player").transform };
        }

    }
}
