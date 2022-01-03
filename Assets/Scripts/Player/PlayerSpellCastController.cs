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

        void Start()
        {
            _inputManager = InputManager.Instance;
            _spellCater = GetComponent<SpellCaster>();
        }

        void Update()
        {
            if (_inputManager.GetFireInput())
                _spellCater.BeginCast(spellConfig);
        }
    }

}