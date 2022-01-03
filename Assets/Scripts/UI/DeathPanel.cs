using UnityEngine.SceneManagement;

namespace Mahou.UI
{
    class DeathPanel : MenuPanel
    {

        public void UI_Retry()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            GameManager.Instance.Paused = false;
        }

        public void UI_MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            GameManager.Instance.Paused = false;
        }

    }
}
