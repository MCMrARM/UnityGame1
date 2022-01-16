using Mahou.Config;
using UnityEngine;

namespace Mahou
{
    public class PlayerInventoryController : MonoBehaviour
    {
        public delegate void AddItemAction(ItemConfig item, int count);
        public AddItemAction onAddItem;


        public void AddItem(ItemConfig item, int count)
        {
            if (onAddItem != null)
                onAddItem(item, count);
        }

    }
}
