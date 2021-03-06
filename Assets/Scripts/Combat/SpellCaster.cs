using Mahou.Config;
using System;
using UnityEngine;

namespace Mahou.Combat
{

    public class SpellCaster : MonoBehaviour
    {

        private StatManager _statManager;
        private Collider _shootIgnoreCollider;
        private Transform[] _locations;
        private LocationOptions[] _locationOptions;

        [SerializeField]
        private SpellCastLocation[] _editorLocationKeys;
        [SerializeField]
        private Transform[] _editorLocationValues;

        private SpellCDManager _spellCDManager = new SpellCDManager();
        private SpellConfig _castingSpell = null;
        private float _castingSpellStartTime;
        private Vector3 _castingSpellTarget;

        public bool projectilesCanHurtEnemies;

        public SpellConfig CastingSpell
        {
            get => _castingSpell;
        }

        private void Start()
        {
            _statManager = GetComponent<StatManager>();
            _shootIgnoreCollider = GetComponent<Collider>();
            _locations = new Transform[(int)SpellCastLocation.Count];
            _locationOptions = new LocationOptions[(int)SpellCastLocation.Count];
            for (int i = Math.Min(_editorLocationKeys.Length, _editorLocationValues.Length) - 1; i >= 0; --i)
            {
                _locations[(int)_editorLocationKeys[i]] = _editorLocationValues[i];
                _locationOptions[(int)_editorLocationKeys[i]] = _editorLocationValues[i].GetComponent<LocationOptions>();
            }
        }

        public float GetCD(SpellConfig spell)
        {
            return _spellCDManager.GetCD(spell);
        }

        public bool CanCast(SpellConfig spell)
        {
            return _spellCDManager.CanCast(spell);
        }

        public virtual bool BeginCast(SpellConfig spell, Vector3 target)
        {
            if (_castingSpell == spell)
                return true;
            if (!CanCast(spell))
                return false;

            _castingSpell = spell;
            _castingSpellStartTime = Time.time;
            _castingSpellTarget = target;
            if (CanFinishCast())
                FinishCast();
            return true;
        }

        public void CancelCast()
        {
            _castingSpell = null;
        }

        protected bool HasMinimumCastTimePassed()
        {
            return _castingSpell != null && Time.time > _castingSpellStartTime + _castingSpell.castTime;
        }

        protected virtual bool CanFinishCast()
        {
            return HasMinimumCastTimePassed();
        }

        protected virtual void FinishCast()
        {
            var spell = _castingSpell;
            _spellCDManager.OnCast(spell);

            if (spell.type == SpellType.Projectile)
            {
                var pspell = (ProjectileSpellConfig) spell;
                SpawnProjectile(pspell.prefab, _locations[(int)pspell.launchLocation], _castingSpellTarget, _locationOptions[(int)pspell.launchLocation], pspell.projectileSpeed);
            }
            else if (spell.type == SpellType.AttackPrefab)
            {
                var pspell = (AttackPrefabSpellConfig)spell;
                SpawnAttackPrefab(pspell.prefab, _locations[(int)pspell.spawnLocation], _locationOptions[(int)pspell.spawnLocation], pspell.spawnDistance);
            }

            _castingSpell = null;
        }

        private void Update()
        {
            if (CanFinishCast())
            {
                FinishCast();
            }
        }

        private void SpawnProjectile(GameObject prefab, Transform shootPoint, Vector3 targetPoint, LocationOptions shootPointOptions, float shootSpeed)
        {
            var rot = shootPoint.rotation.eulerAngles;
            if (shootPointOptions.useModelRotX)
                rot.x = transform.rotation.eulerAngles.x;
            if (shootPointOptions.useModelRotY)
                rot.y = transform.rotation.eulerAngles.y;
            var projectile = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            if (shootPointOptions.canTargetDirect)
                projectile.GetComponent<Rigidbody>().velocity = (targetPoint - shootPoint.position).normalized * shootSpeed;
            else
                projectile.GetComponent<Rigidbody>().velocity = (Quaternion.Euler(rot) * Vector3.forward) * shootSpeed;
            //projectile.transform.position += (Quaternion.Euler(rot) * Vector3.forward);
            var projectileInfo = projectile.GetComponent<ProjectileHitHandler>();
            projectileInfo.attacker = _statManager;
            projectileInfo.dmg = _statManager.CalculateAttack(projectileInfo.damageType);
            projectileInfo.ignoreCollider = _shootIgnoreCollider;
            projectileInfo.canHurtEnemies = projectilesCanHurtEnemies;
        }

        private void SpawnAttackPrefab(GameObject prefab, Transform spawnPoint, LocationOptions spawnPointOptions, float spawnDistance)
        {
            var rot = spawnPoint.rotation.eulerAngles;
            if (spawnPointOptions != null && spawnPointOptions.useModelRotX)
                rot.x = transform.rotation.eulerAngles.x;
            if (spawnPointOptions != null && spawnPointOptions.useModelRotY)
                rot.y = transform.rotation.eulerAngles.y;
            var instance = Instantiate(prefab, spawnPoint.position, Quaternion.Euler(rot));
            instance.transform.position += spawnDistance * (Quaternion.Euler(rot) * Vector3.forward);
        }

    }
}
