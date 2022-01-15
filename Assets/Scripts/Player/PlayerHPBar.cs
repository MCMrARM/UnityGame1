using Mahou.Combat;
using UnityEngine;

namespace Mahou
{
    public class PlayerHPBar : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI[] hpLabel;
        [SerializeField]
        private TMPro.TextMeshProUGUI[] levelLabel;
        [SerializeField]
        private RectTransform hpBarValTransform;
        [SerializeField]
        private RectTransform hpBarValParentTransform;

        public Animator lvUpAnim;
        private int _lastLevel = 1;

        public StatManager statManager;

        void Update()
        {
            foreach (var lbl in levelLabel)
                lbl.text = "Lv. " + statManager.level;
            foreach (var lbl in hpLabel)
                lbl.text = statManager.hp + "/" + statManager.MaxHp;

            float hpPercent = statManager.MaxHp > 0f ? statManager.hp / statManager.MaxHp : 0f;
            hpBarValTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpBarValParentTransform.sizeDelta.x * hpPercent);

            if (statManager.level > _lastLevel)
            {
                lvUpAnim.SetTrigger("LevelUp");
                _lastLevel = statManager.level;
            }
        }
    }

}