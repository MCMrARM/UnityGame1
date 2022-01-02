using UnityEngine;

namespace Mahou.Config
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spell CD Group", order = 1)]
    public class SpellCooldownGroup : ScriptableObject
    {
        public float cooldown;
    }
}
