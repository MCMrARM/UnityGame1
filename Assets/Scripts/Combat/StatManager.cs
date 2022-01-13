using UnityEngine;

namespace Mahou.Combat
{
    public class StatManager : MonoBehaviour
    {
        public BaseStatData baseStats = BaseStatData.Default;

        public int level = 1;
        public float hp;

        public float MaxHp
        {
            get => baseStats.HpBase.Compute(level);
        }

        public bool Alive
        {
            get => hp > 0;
        }

        private void Start()
        {
            hp = MaxHp;
        }

        public float CalculateAttack(DamageType type)
        {
            float atkBase = baseStats.AtkBase.Compute(level);
            float atkTypeMul = baseStats.GetAtkMul(type, level);
            return atkBase * atkTypeMul;
        }

        public string GetAttackDebugString(DamageType type)
        {
            float atkBase = baseStats.AtkBase.Compute(level);
            float atkTypeMul = baseStats.GetAtkMul(type, level);
            return "ATK=(" + atkBase + "*" + atkTypeMul + ")";
        }

        public float CalculateDamage(DamageType type, float dmg, StatManager attacker)
        {
            if (attacker == null)
            {
                Debug.Log("Damage: [Attacker not specified] FINAL=" + dmg);
                return dmg;
            }

            float defBase = baseStats.DefBase.Compute(level);
            float defTypeMul = baseStats.GetDefMul(type, level);
            float def = defBase * defTypeMul;

            float atk = attacker.CalculateAttack(type);
            string atkDebug = attacker.GetAttackDebugString(type);
            float final = 2 * dmg * (atk / (atk + def));
            Debug.Log("Damage: " +
                "DMG=" + dmg + " " +
                atkDebug + " " +
                "DEF=(" + defBase + "*" + defTypeMul + ") " +
                "FINAL=" + final);
            return final;
        }

    }
}
