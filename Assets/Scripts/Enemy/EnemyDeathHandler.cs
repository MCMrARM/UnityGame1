using UnityEngine;

namespace Mahou
{
    class EnemyDeathHandler : MonoBehaviour
    {
        public delegate void DeathAction();
        public DeathAction onDeath;
        private bool deathTriggered;

        public void KillAndDestroy()
        {
            if (!deathTriggered)
            {
                onDeath();
                Destroy(gameObject);
                deathTriggered = true;
            }
        }
    }
}
