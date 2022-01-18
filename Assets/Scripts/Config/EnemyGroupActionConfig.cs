using System;
using UnityEngine;

namespace Mahou.Config
{
    public enum EnemyGroupActionType
    {
        ExecuteAnimatorTrigger
    }

    [Serializable]
    public class EnemyGroupActionConfig
    {

        public EnemyGroupActionType type;
        public GameObject target;
        public string param;

    }
}
