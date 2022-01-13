using System;

namespace Mahou.Combat
{
    [Serializable]
    public struct BaseStatData
    {
        public static readonly BaseStatData Default = new BaseStatData()
        {
            HpBase = new BaseStatCurve(50, 2000, 5000),
            AtkBase = new BaseStatCurve(10, 400, 1000),
            AtkMulPhys = BaseStatCurve.One,
            AtkMulFire = BaseStatCurve.One,
            AtkMulIce = BaseStatCurve.One,
            DefBase = new BaseStatCurve(10, 400, 1000),
            DefMulPhys = BaseStatCurve.One,
            DefMulFire = BaseStatCurve.One,
            DefMulIce = BaseStatCurve.One
        };

        public BaseStatCurve HpBase;

        public BaseStatCurve AtkBase;
        public BaseStatCurve AtkMulPhys;
        public BaseStatCurve AtkMulFire;
        public BaseStatCurve AtkMulIce;

        public BaseStatCurve DefBase;
        public BaseStatCurve DefMulPhys;
        public BaseStatCurve DefMulFire;
        public BaseStatCurve DefMulIce;

        public float GetAtkMul(DamageType type, int level)
        {
            switch (type)
            {
                case DamageType.Physical:
                    return AtkMulPhys.Compute(level);
                case DamageType.Fire:
                    return AtkMulFire.Compute(level);
                case DamageType.Ice:
                    return AtkMulIce.Compute(level);
                default:
                    return 1f;
            }
        }


        public float GetDefMul(DamageType type, int level)
        {
            switch (type)
            {
                case DamageType.Physical:
                    return DefMulPhys.Compute(level);
                case DamageType.Fire:
                    return DefMulFire.Compute(level);
                case DamageType.Ice:
                    return DefMulIce.Compute(level);
                default:
                    return 1f;
            }
        }
    }
}
