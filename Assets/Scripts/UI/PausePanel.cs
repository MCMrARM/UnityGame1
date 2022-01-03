using UnityEngine;

namespace Mahou.UI
{
    class PausePanel : MonoBehaviour
    {

        public void UI_Unpause()
        {
            GameManager.Instance.Paused = false;
        }

        public void UI_Quit()
        {
            Application.Quit();
        }

    }
}
