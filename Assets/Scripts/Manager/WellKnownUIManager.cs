using System.Collections.Generic;
using UnityEngine.Events;

namespace Mahou
{
    public enum WellKnownUIType
    {
        None,
        DeathPanel,
        WinPanel
    }

    public class WellKnownUIManager : Singleton<WellKnownUIManager>
    {

        private HashSet<WellKnownUIType> shownUIs = new HashSet<WellKnownUIType>();
        public UnityAction changeAction;

        public bool IsShown(WellKnownUIType uiType)
        {
            return shownUIs.Contains(uiType);
        }

        public void ShowUI(WellKnownUIType uiType)
        {
            shownUIs.Add(uiType);
            if (changeAction != null)
                changeAction();
        }

        public void HideUI(WellKnownUIType uiType)
        {
            shownUIs.Remove(uiType);
            if (changeAction != null)
                changeAction();
        }

    }
}
