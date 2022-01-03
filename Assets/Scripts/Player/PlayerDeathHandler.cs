using Mahou.Combat;
using UnityEngine;

namespace Mahou
{
    class PlayerDeathHandler : MonoBehaviour
    {

        private StatManager _statManager;
        public GameObject deathPanel;

        private void Start()
        {
            _statManager = GetComponent<StatManager>();
        }

        public void Update()
        {
            if (!_statManager.Alive)
            {
                GameManager.Instance.Paused = false;
                deathPanel.SetActive(true);
            }
        }

    }
}
