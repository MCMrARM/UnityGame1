using Mahou.Combat;
using Mahou.Config;
using UnityEngine;

namespace Mahou
{
    class SimpleFrozenSpecialEffect : BaseSpecialEffectApplier
    {
        public GameObject model;
        private GameObject clonedModel;
        public Material frozenMaterial;
        public Animator freezeAnimator;

        protected override void OnEffectAdded(AreaEffectSpecialEffectType type)
        {
            if (type != AreaEffectSpecialEffectType.Frozen)
                return;

            clonedModel = Instantiate(model, model.transform.parent);
            clonedModel.name = model.name + "(Frozen)";

            var renderer = clonedModel.GetComponent<Renderer>();
            var newMats = new Material[renderer.sharedMaterials.Length];
            for (var i = 0; i < newMats.Length; i++)
                newMats[i] = frozenMaterial;
            renderer.materials = newMats;

            freezeAnimator.enabled = false;
        }

        protected override void OnEffectRemoved(AreaEffectSpecialEffectType type)
        {
            if (type != AreaEffectSpecialEffectType.Frozen)
                return;

            if (clonedModel != null)
                Destroy(clonedModel);

            freezeAnimator.enabled = true;
        }
    }
}
