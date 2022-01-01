using Mahou.Combat;
using System.Collections.Generic;
using UnityEngine;


namespace Mahou
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI levelLabel;
        [SerializeField]
        private RectTransform hpBarValTransform;
        [SerializeField]
        private RectTransform hpBarValParentTransform;
        [SerializeField]
        private RectTransform hpBarLagValTransform;

        public StatManager statManager;
        private LinkedList<HPLog> hpLog = new LinkedList<HPLog>();
        public float hpLagTime;
        private float smoothLaggedHpPercent;

        private void Start()
        {
            levelLabel.text = "Lv. " + statManager.level;
        }

        private float GetLaggedHP()
        {
            while (hpLog.Count > 0 && Time.time >= hpLog.First.Value.Time + hpLagTime)
            {
                hpLog.RemoveFirst();
            }
            return hpLog.Count > 0 ? hpLog.First.Value.HP : (-1f);
        }

        private void UpdateLaggedHP(float newHp)
        {
            if (hpLog.Count > 0 && newHp > hpLog.Last.Value.HP)
            {
                hpLog.Clear();
            }
            if (hpLog.Count > 0 && newHp <= hpLog.Last.Value.HP)
            {
                hpLog.Last.Value.HP = newHp;
                hpLog.Last.Value.Time = Time.time;
            }
            else
            {
                hpLog.AddLast(new HPLog { HP = newHp, Time = Time.time });
            }
        }

        private void Update()
        {
            float hpPercent = statManager.MaxHp > 0f ? statManager.hp / statManager.MaxHp : 0f;
            hpBarValTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpBarValParentTransform.sizeDelta.x * hpPercent);

            float laggedHp = GetLaggedHP();
            float laggedHpPercent = statManager.MaxHp > 0f ? laggedHp / statManager.MaxHp : 0f;
            smoothLaggedHpPercent = Mathf.Max(smoothLaggedHpPercent, laggedHpPercent);
            smoothLaggedHpPercent = Mathf.MoveTowards(smoothLaggedHpPercent, laggedHpPercent, Time.deltaTime);
            hpBarLagValTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hpBarValParentTransform.sizeDelta.x * smoothLaggedHpPercent);

            UpdateLaggedHP(statManager.hp);
        }

        private class HPLog
        {
            public float HP;
            public float Time;
        }

    }

}