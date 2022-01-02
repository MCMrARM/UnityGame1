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

        public bool CanCast(SpellConfig spell)
        {
            return _spellCDManager.CanCast(spell);
        }

        public virtual bool BeginCast(SpellConfig spell)
        {
            if (_castingSpell == spell)
                return true;
            if (!CanCast(spell))
                return false;

            _castingSpell = spell;
            _castingSpellStartTime = Time.time;
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
                SpawnProjectile(pspell.prefab, _locations[(int)pspell.launchLocation], _locationOptions[(int)pspell.launchLocation], pspell.projectileSpeed);
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

        private void SpawnProjectile(GameObject prefab, Transform shootPoint, LocationOptions shootPointOptions, float shootSpeed)
        {
            var rot = shootPoint.rotation.eulerAngles;
            if (shootPointOptions.useModelRotX)
                rot.x = transform.rotation.eulerAngles.x;
            var projectile = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            //projectile.transform.position += (Quaternion.Euler(rot) * Vector3.forward);
            projectile.GetComponent<Rigidbody>().velocity = (Quaternion.Euler(rot) * Vector3.forward)  * shootSpeed;
            var projectileInfo = projectile.GetComponent<ProjectileHitHandler>();
            projectileInfo.attacker = _statManager;
            projectileInfo.dmg = _statManager.CalculateAttack(projectileInfo.damageType);
            projectileInfo.ignoreCollider = _shootIgnoreCollider;
        }

    }
}
