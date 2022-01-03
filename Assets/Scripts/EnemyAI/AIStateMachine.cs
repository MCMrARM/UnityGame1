using Mahou;
using Mahou.Combat;
using UnityEngine;

namespace Mahou.EnemyAI
{
    public class AIStateMachine : MonoBehaviour
    {
        public SpellCaster spellCaster;
        public Rigidbody rigidbody;
        private AIState _currentState;
        public float moveSpeed = 2f;
        public float turnSpeed = 2f;

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
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
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
