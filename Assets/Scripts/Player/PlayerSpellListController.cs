using Mahou.Config;
using UnityEngine;
using UnityEngine.Events;

namespace Mahou
{
    public class PlayerSpellListController : MonoBehaviour
    {
        private InputManager _inputManager;
        public SpellConfig[] spellList;
        public int activeSpellIndex = 0;
        public UnityAction activeSpellChange;

        public SpellConfig activeSpell
        {
            get => spellList[activeSpellIndex];
        }

        void Start()
        {
            _inputManager = InputManager.Instance;
        }

        void Update()
        {
            var input = _inputManager.GetSpellSwitchInput();
            if (input >= 1 && input <= spellList.Length)
            {
                activeSpellIndex = input - 1;
                activeSpellChange();
            }
        }
    }

}