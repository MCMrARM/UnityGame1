using Mahou.Combat;
using Mahou.Config;
using UnityEngine;

namespace Mahou
{

    public class ProjectileHitHandler : MonoBehaviour
    {
        public float lifetime;
        public DamageType damageType;
        public Collider ignoreCollider;

        [HideInInspector]
        public float dmg;
        [HideInInspector]
        public StatManager attacker;


        void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == ignoreCollider)
                return;
            if (Tags.IsValidTarget(other.gameObject.tag))
            {
                Debug.Log("OnTriggerEnter " + other.name);
                var dmgReceiver = other.gameObject.GetComponent<DamageReceiverBase>();
                if (dmgReceiver != null)
                    dmgReceiver.OnReceiveDamage(damageType, dmg, attacker);

                Destroy(gameObject);
            }
        }
    }


}