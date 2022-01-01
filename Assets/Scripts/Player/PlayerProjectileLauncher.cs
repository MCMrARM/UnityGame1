using Mahou.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mahou
{

    public class PlayerProjectileLauncher : MonoBehaviour
    {
        private StatManager _statManager;
        public GameObject projectilePrefab;
        public Transform shootPoint;
        public float shootSpeed;

        void Start()
        {
            _statManager = GetComponent<StatManager>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SpawnProjectile(projectilePrefab);
            }
        }

        void SpawnProjectile(GameObject prefab)
        {
            var projectile = Instantiate(prefab, shootPoint.position, shootPoint.rotation);
            projectile.transform.position += shootPoint.TransformVector(Vector3.forward);
            projectile.GetComponent<Rigidbody>().velocity = shootPoint.TransformVector(Vector3.forward) * shootSpeed;
            var projectileInfo = projectile.GetComponent<ProjectileHitHandler>();
            projectileInfo.attacker = _statManager;
            projectileInfo.dmg = _statManager.CalculateAttack(projectileInfo.damageType);
        }
    }

}