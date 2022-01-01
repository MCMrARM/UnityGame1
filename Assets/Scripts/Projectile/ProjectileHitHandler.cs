using Mahou.Combat;
using Mahou.Config;
using UnityEngine;

namespace Mahou
{

    public class ProjectileHitHandler : MonoBehaviour
    {
        public float lifetime;
        public DamageType damageType;

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
            if (Tags.IsValidTarget(other.gameObject.tag))
            {
                Destroy(gameObject);

                var dmgReceiver = other.gameObject.GetComponent<DamageReceiverBase>();
                if (dmgReceiver != null)
                    dmgReceiver.OnReceiveDamage(damageType, dmg, attacker);
            }
        }
    }


}