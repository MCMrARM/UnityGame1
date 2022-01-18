using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mahou.UI
{
    class PausePanel : MonoBehaviour
    {

        public void UI_Unpause()
        {
            GameManager.Instance.Paused = false;
        }

        public void UI_Reload()
        {
            var scene = SceneManager.GetActiveScene();
            GameManager.Instance.LoadIntoLevel(scene.name);
        }

        public void UI_QuitToMenu()
        {
            GameManager.Instance.LoadMainMenu();
        }

    }
}
