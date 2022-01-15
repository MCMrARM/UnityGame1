using Mahou.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Mahou.UI
{
    public class ObtainedItemEntryUI : MonoBehaviour
    {
        public RawImage uiIcon;
        public TMPro.TextMeshProUGUI uiName;
        public TMPro.TextMeshProUGUI uiNameBg;


        public void SetItem(ItemConfig item, int count)
        {
            uiIcon.texture = item.uiIcon;
            uiName.text = count + " " + item.uiName;
            uiNameBg.text = uiName.text;
        }

        public void BeginShow()
        {
            GetComponent<Animator>().SetTrigger("Show");
        }

        public void BeginHide()
        {
            GetComponent<Animator>().SetTrigger("Hide");
        }

    }
}