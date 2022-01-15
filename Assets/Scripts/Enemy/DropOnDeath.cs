using Mahou.Combat;
using Mahou.Config;
using UnityEngine;

namespace Mahou
{
    [RequireComponent(typeof(StatManager), typeof(EnemyDeathHandler))]
    public class DropOnDeath : MonoBehaviour
    {

        private StatManager _statManager;
        public DropItemConfig[] dropItems;


        private void Start()
        {
            _statManager = GetComponent<StatManager>();
            GetComponent<EnemyDeathHandler>().onDeath += OnDeath;
        }

        private void OnDeath()
        {
            foreach (var item in dropItems)
            {
                var count = item.dropAmount.Compute(_statManager.level);
                PlayerInventoryController.Instance.AddItem(item.item, (int) Mathf.Round(count));
            }
        }

    }
}
