using Mahou.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Mahou.Combat
{
    class SpellCDManager
    {

        private Dictionary<SpellCooldownGroup, float> spellCooldowns = new Dictionary<SpellCooldownGroup, float>();

        public bool CanCast(SpellConfig spell)
        {
            float val;
            if (!spellCooldowns.TryGetValue(spell.cooldownGroup, out val))
                return true;
            return (Time.time > val);
        }

        public void OnCast(SpellConfig spell)
        {
            spellCooldowns[spell.cooldownGroup] = Time.time + spell.cooldownGroup.cooldown;
        }

    }
}
