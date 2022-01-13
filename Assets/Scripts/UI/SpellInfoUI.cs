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
        public GameObject activeArrowImage;
        private Vector3 _basePos;
        public float activeIndent;

        public void SavePositionForIndent()
        {
            _basePos = transform.localPosition;
        }

        public void SetSpell(SpellCaster caster, SpellConfig spell)
        {
            var cooldown = GetComponent<CooldownDisplay>();
            cooldown.caster = caster;
            cooldown.spell = spell;

            nameLabel.text = spell.uiName;
            iconImage.texture = spell.uiIcon;
            activeArrowImage.SetActive(false);
        }
        
        public void SetSelected(bool selected)
        {
            var newPos = _basePos;
            if (selected)
                newPos.x -= activeIndent;
            transform.localPosition = newPos;
            activeArrowImage.SetActive(selected);
        }
    }
}
