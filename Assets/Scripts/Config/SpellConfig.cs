using UnityEngine;

namespace Mahou.Config
{
    public enum SpellCastLocation
    {
        ProjectilePrimary = 0,
        CasterPosition,

        Count
    }


    public enum SpellType
    {
        Projectile,
        AttackPrefab
    }

    public class SpellConfig : ScriptableObject
    {
        [HideInInspector]
        public SpellType type;
        public float castTime;
        public SpellCooldownGroup cooldownGroup;
    }

}
