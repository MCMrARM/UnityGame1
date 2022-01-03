using UnityEngine;

namespace Mahou.UI
{
    class PauseVisibility : MonoBehaviour
    {

        public GameObject[] showOnPause;
        public GameObject[] hideOnPause;

        private void OnEnable()
        {
            GameManager.Instance.PauseAction += UpdatePause;
            UpdatePause();
        }

        private void OnDisable()
        {
            GameManager.Instance.PauseAction -= UpdatePause;
        }

        private void UpdatePause()
        {
            foreach (GameObject obj in showOnPause)
                obj.SetActive(GameManager.Instance.Paused);
            foreach (GameObject obj in hideOnPause)
                obj.SetActive(!GameManager.Instance.Paused);
        }

    }
}
