
using Mahou.Config;
using UnityEngine;

namespace Mahou.EnemyAI
{
    class AIFoeLocator : MonoBehaviour
    {

        public float updateInterval = 0.25f;
        public float range = 10f;


        private float nextUpdateTime = 0f;
        private GameObject foe;

        private void Update()
        {
            if (Time.time >= nextUpdateTime)
            {
                ForceUpdateClosestFoe();
            }
        }

        public GameObject GetClosestFoe()
        {
            return foe;
        }

        public GameObject ForceUpdateClosestFoe()
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, LayerMasks.MaskPlayerAllied);
            GameObject closestObject = null;
            float closestObjectDist = 1000000f;
            foreach (Collider c in enemiesInRange)
            {
                float dist = Vector3.Distance(c.gameObject.transform.position, transform.position);
                if (dist < closestObjectDist)
                {
                    closestObject = c.gameObject;
                    closestObjectDist = dist;
                }
            }
            nextUpdateTime = Time.time + updateInterval;
            foe = closestObject;
            return closestObject;
        }

    }
}
