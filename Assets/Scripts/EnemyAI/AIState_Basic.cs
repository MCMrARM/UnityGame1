using Mahou.Config;
using System;
using UnityEngine;

namespace Mahou.EnemyAI
{

    public class AIState_MoveTowards : AIState
    {

        public Vector3 target;

        public override bool Update()
        {
            return MoveTowards(target);
        }

    }


}
