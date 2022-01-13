using Mahou.Config;
using UnityEngine;

namespace Mahou.Combat
{
    public abstract class BaseSpecialEffectApplier : MonoBehaviour
    {

        private EffectManager _effectManager;

        private void OnEnable()
        {
            _effectManager = GetComponent<EffectManager>();
            _effectManager.SpecialEffectChange += OnSpecialEffectChange;
        }

        private void OnDisable()
        {
            _effectManager.SpecialEffectChange -= OnSpecialEffectChange;
        }

        private void OnSpecialEffectChange(AreaEffectSpecialEffectType type, bool added)
        {
            if (added)
                OnEffectAdded(type);
            else
                OnEffectRemoved(type);
        }

        protected abstract void OnEffectAdded(AreaEffectSpecialEffectType type);

        protected abstract void OnEffectRemoved(AreaEffectSpecialEffectType type);

    }
}
