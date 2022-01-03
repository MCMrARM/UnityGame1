using UnityEngine;

namespace Mahou.Config
{
    public class LayerMasks
    {
        public static readonly int MaskEnemy = LayerMask.GetMask("Enemy");
        public static readonly int MaskPlayerAllied = LayerMask.GetMask("PlayerAllied");
        public const int MaskPlayerEnemyRaycast = 0x7fffffff;

    }
}
