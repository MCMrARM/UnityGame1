using Mahou.Combat;
using Mahou.Config;
using UnityEngine;

namespace Mahou
{

    public class PlayerSpellCastController : MonoBehaviour
    {
        private InputManager _inputManager;
        private SpellCaster _spellCater;
        public SpellConfig spellConfig;
        public Transform baseTransform;

        void Start()
        {
            _inputManager = InputManager.Instance;
            _spellCater = GetComponent<SpellCaster>();
        }

        void Update()
        {
            if (_inputManager.GetFireInput() || Application.isEditor)
            {
                float maxDist = 100f;
                Vector3 dir = baseTransform.TransformDirection(Vector3.forward);
                Vector3 target = dir * maxDist;
                RaycastHit hit;
                if (Physics.Raycast(baseTransform.position, dir, out hit, maxDist, LayerMasks.MaskPlayerEnemyRaycast))
                {
                    Debug.DrawRay(baseTransform.position, dir * hit.distance, Color.yellow);
                    target = dir * hit.distance;
                }
                if (_inputManager.GetFireInput())
                    _spellCater.BeginCast(spellConfig, baseTransform.position + target);
            }
        }
    }

}