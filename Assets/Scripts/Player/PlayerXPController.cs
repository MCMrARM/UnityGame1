using Mahou.Combat;
using Mahou.Config;
using UnityEngine;

namespace Mahou
{
    [RequireComponent(typeof(StatManager), typeof(PlayerInventoryController))]
    class PlayerXPController : MonoBehaviour
    {
        private StatManager _statManager;
        public ItemConfig xpItem;
        public BaseStatCurve xpPerLevel;
        [SerializeField]
        private int _level = 1;
        [SerializeField]
        private int _xp;

        private void Start()
        {
            _statManager = GetComponent<StatManager>();
            GetComponent<PlayerInventoryController>().onAddItem += OnAddItem;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad8))
                OnAddItem(xpItem, 50);
        }
#endif

        private void OnAddItem(ItemConfig item, int amount)
        {
            if (item == xpItem)
            {
                _xp += amount;
                Debug.Log("Add XP: " + _xp + "/" + xpPerLevel.Compute(_level));
                while (_xp >= xpPerLevel.Compute(_level))
                {
                    ++_level;
                    _xp -= (int)Mathf.Ceil(xpPerLevel.Compute(_level));
                }
                _statManager.level = _level;
                _statManager.hp = _statManager.MaxHp;
            }
        }
    }
}
