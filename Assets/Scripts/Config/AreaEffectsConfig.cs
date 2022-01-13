using Mahou.Combat;
using System;
using UnityEngine;

namespace Mahou.Config
{
    public enum AreaEffectSpecialEffectType
    {
        None,
        Frozen
    }


    [Serializable]
    public struct AreaEffectConfig
    {

        public AreaEffectType type;
        public float accumulationRate;
        public float accumulationLimit;
        public float disperseRate;
        public float disperseDelay;
        public float damageMinAccumulatedValue;
        public float damageInterval;
        public float damageAmountFixed;
        public float damageAmountHpPercent;
        public DamageType damageType;
        public AreaEffectSpecialEffectType specialEffectType;
        public float specialEffectMinAccumulatedValue;

    }


    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AreaEffectsConfig", order = 1)]
    public class AreaEffectsConfig : ScriptableObject
    {

        [SerializeField]
        private AreaEffectConfig[] entries;
        private AreaEffectConfig[] indexedEntriesSource;
        private AreaEffectConfig[] indexedEntries;

        public AreaEffectConfig GetConfig(AreaEffectType type)
        {
            if (indexedEntriesSource != entries)
            {
                indexedEntries = new AreaEffectConfig[(int) AreaEffectType.Count];
                foreach (AreaEffectConfig config in entries)
                    indexedEntries[(int)config.type] = config;
                indexedEntriesSource = entries;
            }
            return indexedEntries[(int)type];
        }


    }
}
