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
            SceneManager.LoadScene(scene.name);
            GameManager.Instance.ResetGame();
        }

        public void UI_QuitToMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
            GameManager.Instance.ResetGame();
        }

    }
}
