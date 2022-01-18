using UnityEngine.SceneManagement;

namespace Mahou.UI
{
    class WinPanel : MenuPanel
    {
        public void UI_MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            GameManager.Instance.ResetGame();
        }

    }
}
