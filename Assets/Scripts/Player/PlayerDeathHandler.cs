using Mahou.Combat;
using UnityEngine;

namespace Mahou
{
    class PlayerDeathHandler : MonoBehaviour
    {

        private StatManager _statManager;

        private void Start()
        {
            _statManager = GetComponent<StatManager>();
        }

        public void Update()
        {
            if (!_statManager.Alive)
            {
                GameManager.Instance.Paused = false;
                WellKnownUIManager.Instance.ShowUI(WellKnownUIType.DeathPanel);
            }
        }

    }
}
