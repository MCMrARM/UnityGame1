using UnityEngine;

namespace Mahou.EnemyAI
{
    [RequireComponent(typeof(AIFoeLocator))]
    class AITargetSelector : MonoBehaviour
    {

        // TODO: Damage based target selection

        private AIFoeLocator _foeLocator;

        private void Start()
        {
            _foeLocator = GetComponent<AIFoeLocator>();
        }

        public GameObject GetTarget()
        {
            return _foeLocator.GetClosestFoe();
        }

    }
}
