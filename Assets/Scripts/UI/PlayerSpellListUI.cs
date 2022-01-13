using Mahou.Combat;
using UnityEngine;

namespace Mahou.UI
{
    public class PlayerSpellListUI : MonoBehaviour
    {
        public PlayerSpellListController spellListController;
        private SpellInfoUI[] _uiSpellInfo;
        public GameObject spellInfoPrefab;
        public float itemSpacing;
        private int _uiActiveSpellIndex = -1;

        void Start()
        {
            RecreateElements();
        }

        private void Update()
        {
            var index = spellListController.activeSpellIndex;
            if (_uiActiveSpellIndex != -1)
            {
                _uiSpellInfo[_uiActiveSpellIndex].SetSelected(false);
            }
            _uiSpellInfo[index].SetSelected(true);
            _uiActiveSpellIndex = index;
        }

        private void RecreateElements()
        {
            if (_uiSpellInfo != null)
            {
                foreach (var obj in _uiSpellInfo)
                    Destroy(obj);
            }

            var caster = spellListController.GetComponent<SpellCaster>();
            var itemCount = spellListController.spellList.Length;
            var itemHeight = spellInfoPrefab.GetComponent<RectTransform>().sizeDelta.y;
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (itemHeight + itemSpacing) * itemCount - itemSpacing);

            _uiSpellInfo = new SpellInfoUI[itemCount];
            for (var i = 0; i < itemCount; i++)
            {
                var spellInfo = Instantiate(spellInfoPrefab, transform);
                spellInfo.transform.localPosition += new Vector3(0, -(itemHeight + itemSpacing) * i);
                var uiComponent = spellInfo.GetComponent<SpellInfoUI>();
                uiComponent.SavePositionForIndent();
                uiComponent.SetSpell(caster, spellListController.spellList[i]);
                _uiSpellInfo[i] = uiComponent;
            }
        }
    }

}