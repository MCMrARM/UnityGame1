using UnityEngine.SceneManagement;

namespace Mahou.UI
{
    class DeathPanel : MenuPanel
    {

        public void UI_Retry()
        {
            var scene = SceneManager.GetActiveScene();
            GameManager.Instance.LoadIntoLevel(scene.name);
        }

        public void UI_MainMenu()
        {
            GameManager.Instance.LoadMainMenu();
        }

    }
}
