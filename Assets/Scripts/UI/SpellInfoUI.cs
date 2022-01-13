using Mahou.Combat;
using Mahou.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Mahou.UI
{
    [RequireComponent(typeof(CooldownDisplay))]
    public class SpellInfoUI : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI nameLabel;
        public RawImage iconImage;

        public void SetSpell(SpellCaster caster, SpellConfig spell)
        {
            var cooldown = GetComponent<CooldownDisplay>();
            cooldown.caster = caster;
            cooldown.spell = spell;

            nameLabel.text = spell.uiName;
            iconImage.texture = spell.uiIcon;
        }
    }
}
