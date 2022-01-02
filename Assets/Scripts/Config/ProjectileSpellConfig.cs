using UnityEngine;

namespace Mahou.Config
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectile Spell", order = 1)]
    public class ProjectileSpellConfig : SpellConfig
    {
        public GameObject prefab;
        public float projectileSpeed;
        public SpellCastLocation launchLocation;

        public ProjectileSpellConfig()
        {
            type = SpellType.Projectile;
        }
    }

}
