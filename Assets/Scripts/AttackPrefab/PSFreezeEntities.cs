using Mahou.Combat;
using Mahou.Config;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Mahou
{
    public class PSFreezeEntities : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private Particle[] _particles = new Particle[1];

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (_particleSystem.GetParticles(_particles, 1, 0) == 1)
            {
                Collider[] enemiesInRange = Physics.OverlapSphere(_particles[0].position, _particles[0].GetCurrentSize(_particleSystem), LayerMasks.MaskEnemy);
                foreach (Collider c in enemiesInRange)
                {
                    EffectManager manager;
                    if (c.gameObject.TryGetComponent<EffectManager>(out manager))
                    {
                        manager.AddAreaEffect(AreaEffectType.Frost, Time.deltaTime);
                    }
                }
            }
        }

    }

}