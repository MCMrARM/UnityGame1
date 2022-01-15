using UnityEngine;

namespace Mahou.Config
{
    public enum ItemType
    {
        DisplayOnly
    }

    public class ItemConfig : ScriptableObject
    {
        [HideInInspector]
        public ItemType type;

        public string uiName;
        public Texture2D uiIcon;
    }

}
