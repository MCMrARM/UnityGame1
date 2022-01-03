using UnityEngine;

namespace Mahou.UI
{
    class MenuPanel : MonoBehaviour
    {

        private void OnEnable()
        {
            InputManager.Instance.OnOpenMenu();
        }

        private void OnDisable()
        {
            InputManager.Instance.OnCloseMenu();
        }

    }
}
