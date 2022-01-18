namespace Mahou.UI
{
    class MainMenuPanel : MenuPanel
    {

        public TMPro.TMP_Dropdown levelNameDropDown;

        public void UI_LoadLevel()
        {
            GameManager.Instance.LoadIntoLevel(levelNameDropDown.options[levelNameDropDown.value].text + "Scene");
        }

    }
}
