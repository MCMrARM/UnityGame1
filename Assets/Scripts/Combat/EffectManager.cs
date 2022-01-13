using Mahou.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Mahou.Combat
{
    [RequireComponent(typeof(DamageReceiverBase))]
    public class EffectManager : MonoBehaviour
    {

        private StatManager _statManager;
        private DamageReceiverBase _dmgRecevier;
        public AreaEffectsConfig areaEffectConfig;
        private float[] areaEffectValues = new float[(int)AreaEffectType.Count];
        private float[] areaEffectDisperseTime = new float[(int)AreaEffectType.Count];
        private float[] areaEffectDmgTime = new float[(int)AreaEffectType.Count];
        private HashSet<AreaEffectSpecialEffectType> enabledSpecialEffects = new HashSet<AreaEffectSpecialEffectType>();
        private bool skipUpdate = true;

        public delegate void SpecialEffectChangeAction(AreaEffectSpecialEffectType type, bool enabled);
        public SpecialEffectChangeAction SpecialEffectChange;

        public void AddAreaEffect(AreaEffectType areaEffectType, float timeAmount)
        {
            var config = areaEffectConfig.GetConfig(areaEffectType);
            areaEffectValues[(int)areaEffectType] = Mathf.Min(areaEffectValues[(int)areaEffectType] + timeAmount * config.accumulationRate, config.accumulationLimit);
            areaEffectDisperseTime[(int)areaEffectType] = Time.time + config.disperseDelay;
            skipUpdate = false;
        }

        private void Start()
        {
            _statManager = GetComponent<StatManager>();
            _dmgRecevier = GetComponent<DamageReceiverBase>();
        }

        private void Update()
        {
            if (skipUpdate)
                return;

            skipUpdate = true;
            for (int i = 0; i < areaEffectValues.Length; i++)
            {
                if (areaEffectValues[i] == 0)
                    continue;

                var config = areaEffectConfig.GetConfig((AreaEffectType)i);

                skipUpdate = false;
                if (Time.time >= areaEffectDisperseTime[i])
                {
                    areaEffectValues[i] = Mathf.Max(areaEffectValues[i] - config.disperseRate * Time.deltaTime, 0);
                }
                if (areaEffectValues[i] > config.damageMinAccumulatedValue)
                {
                    bool inited = (areaEffectDmgTime[i] != 0f);
                    areaEffectDmgTime[i] -= Time.deltaTime;
                    if (areaEffectDmgTime[i] <= 0f)
                    {
                        if (inited)
                        {
                            Debug.Log("Effect damage: [" + config.damageType + "] " + config.damageAmountFixed + " + " + config.damageAmountHpPercent +  " / 100 * " + _statManager.MaxHp);
                            _dmgRecevier.OnReceiveDamage(config.damageType, config.damageAmountFixed + config.damageAmountHpPercent / 100f * _statManager.MaxHp, null);
                        }
                        areaEffectDmgTime[i] = config.damageInterval;
                    }
                }

                if (config.specialEffectType != AreaEffectSpecialEffectType.None)
                {
                    var enabled = areaEffectValues[i] > config.specialEffectMinAccumulatedValue;
                    var wasEnabled = enabledSpecialEffects.Count > 0 && enabledSpecialEffects.Contains(config.specialEffectType);
                    if (enabled != wasEnabled)
                    {
                        if (enabled)
                            enabledSpecialEffects.Add(config.specialEffectType);
                        else
                            enabledSpecialEffects.Remove(config.specialEffectType);

                        SpecialEffectChange(config.specialEffectType, enabled);
                    }
                }
            }
        }

        public bool HasSpecialEffect(AreaEffectSpecialEffectType type)
        {
            return enabledSpecialEffects.Count > 0 && enabledSpecialEffects.Contains(type);
        }

    }
}
