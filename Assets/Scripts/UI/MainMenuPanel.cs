using UnityEngine.SceneManagement;

namespace Mahou.UI
{
    class MainMenuPanel : MenuPanel
    {

        public void UI_LoadLevel()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("SampleScene");
        }

    }
}
