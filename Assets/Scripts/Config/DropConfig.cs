using Mahou.Combat;
using System;

namespace Mahou.Config
{
 
    [Serializable]
    public class DropItemConfig
    {
        public ItemConfig item;
        public BaseStatCurve dropAmount;
    }

}
