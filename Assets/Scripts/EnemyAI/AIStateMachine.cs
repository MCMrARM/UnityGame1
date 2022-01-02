using Mahou;
using Mahou.Combat;
using UnityEngine;

namespace Mahou.EnemyAI
{
    public class AIStateMachine : MonoBehaviour
    {
        public SpellCaster spellCaster;
        private AIState _currentState;

        public AIState CurrentState
        {
            get => _currentState;
            set {
                Debug.Log(gameObject.name + ": AI state [" + _currentState?.GetType().Name + " => " + value?.GetType().Name + "]");
                _currentState = value;
                value.StateMachine = this;
            }
        }

        protected virtual void UpdateState(bool lastComplete)
        {
            //
        }


        private void Start()
        {
            if (spellCaster == null)
                spellCaster = GetComponent<SpellCaster>();
        }

        void Update()
        {
            bool lastComplete = false;
            if (_currentState != null)
            {
                lastComplete = _currentState.Update();
            } else
            {
                lastComplete = true;
            }

            UpdateState(lastComplete);
        }
    }
}
