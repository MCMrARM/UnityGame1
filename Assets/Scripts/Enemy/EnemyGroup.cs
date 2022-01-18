using Mahou.Config;
using UnityEngine;

namespace Mahou
{
    public class EnemyGroup : MonoBehaviour
    {
        public EnemyGroupActionConfig[] allDeadAction;
        private int enemyDeadCounter;

        void Start()
        {
            foreach (var child in transform.GetComponentsInChildren<EnemyDeathHandler>())
            {
                enemyDeadCounter += 1;
                child.onDeath += OnEnemyDeath;
            }
        }

        private void OnEnemyDeath()
        {
            enemyDeadCounter -= 1;
            if (enemyDeadCounter == 0)
            {
                foreach (var action in allDeadAction)
                    ExecuteAction(action);
            }
        }

        private void ExecuteAction(EnemyGroupActionConfig config)
        {
            if (config.type == EnemyGroupActionType.ExecuteAnimatorTrigger)
            {
                config.target.GetComponent<Animator>().SetTrigger(config.param);
            }
        }

    }

}