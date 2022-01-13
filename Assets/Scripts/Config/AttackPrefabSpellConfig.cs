using UnityEngine;

namespace Mahou.Config
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackPrefab Spell", order = 1)]
    public class AttackPrefabSpellConfig : SpellConfig
    {
        public GameObject prefab;
        public SpellCastLocation spawnLocation;
        public float spawnDistance;

        public AttackPrefabSpellConfig()
        {
            type = SpellType.AttackPrefab;
        }
    }

}
