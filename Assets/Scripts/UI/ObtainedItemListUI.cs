using Mahou.Config;
using UnityEngine;
using System.Collections.Generic;

namespace Mahou.UI
{
    public class ObtainedItemListUI : MonoBehaviour
    {
        public GameObject itemGainBoxPrefab;
        public int maxItemsAtTime;
        private ObtainedItemEntryUI[] gainedItemBoxes;
        private Queue<QueuedItemAdd> queuedItemAdds = new Queue<QueuedItemAdd>();
        private int shownItems;
        private float itemHeight;
        private float startHideItemsTime;
        private float hidingItemsTime;
        public float hideItemAnimTime;
        private bool animatorShowTriggered;
        private PlayerInventoryController _playerInventory;

        public ItemConfig testItemConfig;

        void Start()
        {
            gainedItemBoxes = new ObtainedItemEntryUI[maxItemsAtTime];
            itemHeight = itemGainBoxPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        private void OnEnable()
        {
            _playerInventory = PlayerInventoryController.Instance;
            _playerInventory.onAddItem += AddItem;
        }

        private void OnDisable()
        {
            _playerInventory.onAddItem -= AddItem;
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Keypad7))
                AddItem(testItemConfig, 5);
#endif
            if (startHideItemsTime != 0f && Time.time >= startHideItemsTime)
            {
                hidingItemsTime = hideItemAnimTime;
                startHideItemsTime = 0f;
                for (var i = 0; i < shownItems; i++)
                    gainedItemBoxes[i].BeginHide();
                if (queuedItemAdds.Count == 0)
                {
                    GetComponent<Animator>().SetTrigger("Hide");
                    animatorShowTriggered = false;
                }
            }
            if (hidingItemsTime > 0f)
            {
                hidingItemsTime -= Time.deltaTime;
                if (hidingItemsTime <= 0f)
                {
                    shownItems = 0;
                    hidingItemsTime = 0f;
                }
            }

            while (queuedItemAdds.Count > 0)
            {
                var item = queuedItemAdds.Peek();
                if (TryAddItem(item.item, item.count))
                    queuedItemAdds.Dequeue();
                else
                    break;
            }
        }

        private bool TryAddItem(ItemConfig item, int count)
        {
            if (shownItems == maxItemsAtTime || hidingItemsTime > 0f)
                return false;

            Debug.Log("AddItem");
            if (gainedItemBoxes[shownItems] == null)
            {
                var instance = Instantiate(itemGainBoxPrefab, transform);
                instance.transform.position += new Vector3(0, -shownItems * itemHeight);
                gainedItemBoxes[shownItems] = instance.GetComponent<ObtainedItemEntryUI>();
            }
            gainedItemBoxes[shownItems].SetItem(item, count);
            gainedItemBoxes[shownItems].BeginShow();
            if (!animatorShowTriggered)
            {
                GetComponent<Animator>().SetTrigger("Show");
                animatorShowTriggered = true;
            }
            ++shownItems;
            startHideItemsTime = Time.time + 2;
            return true;
        }

        public void AddItem(ItemConfig item, int count)
        {
            if (!TryAddItem(item, count))
            {
                queuedItemAdds.Enqueue(new QueuedItemAdd() { item = item, count = count });
                return;
            }
        }

        private struct QueuedItemAdd
        {
            public ItemConfig item;
            public int count;
        }

    }

}