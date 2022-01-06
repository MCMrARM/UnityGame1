using UnityEngine.SceneManagement;

namespace Mahou.UI
{
    class MainMenuPanel : MenuPanel
    {

        public TMPro.TMP_Dropdown levelNameDropDown;

        public void UI_LoadLevel()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(levelNameDropDown.options[levelNameDropDown.value].text + "Scene");
        }

    }
}
