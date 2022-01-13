using Mahou.Combat;
using UnityEngine;

namespace Mahou.UI
{
    public class PlayerSpellListUI : MonoBehaviour
    {
        public PlayerSpellListController spellListController;
        private GameObject[] _uiSpellInfoObjects;
        public GameObject spellInfoPrefab;
        public float itemSpacing;

        void Start()
        {
            RecreateElements();
        }

        private void RecreateElements()
        {
            if (_uiSpellInfoObjects != null)
            {
                foreach (var obj in _uiSpellInfoObjects)
                    Destroy(obj);
            }

            var caster = spellListController.GetComponent<SpellCaster>();
            var itemCount = spellListController.spellList.Length;
            var itemHeight = spellInfoPrefab.GetComponent<RectTransform>().sizeDelta.y;
            _uiSpellInfoObjects = new GameObject[itemCount];
            for (var i = 0; i < itemCount; i++)
            {
                var spellInfo = Instantiate(spellInfoPrefab, transform);
                spellInfo.transform.localPosition += new Vector3(0, -(itemHeight + itemSpacing) * i);
                var uiComponent = spellInfo.GetComponent<SpellInfoUI>();
                uiComponent.SetSpell(caster, spellListController.spellList[i]);
                _uiSpellInfoObjects[i] = spellInfo;
            }
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (itemHeight + itemSpacing) * itemCount - itemSpacing);
        }
    }

}