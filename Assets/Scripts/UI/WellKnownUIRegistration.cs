using System;
using UnityEngine;

namespace Mahou.UI
{
    class WellKnownUIRegistration : MonoBehaviour
    {

        [Serializable]
        public struct Entry
        {
            public WellKnownUIType type;
            public GameObject uiObject;
        }

        public Entry[] entries;

        private void OnEnable()
        {
            WellKnownUIManager.Instance.changeAction += UpdateVisibility;
            UpdateVisibility();
        }

        private void OnDisable()
        {
            WellKnownUIManager.Instance.changeAction -= UpdateVisibility;
        }

        private void UpdateVisibility()
        {
            var instance = WellKnownUIManager.Instance;
            foreach (var entry in this.entries)
                entry.uiObject.SetActive(instance.IsShown(entry.type));
        }

    }
}
