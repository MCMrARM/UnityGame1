using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Mahou
{
    class GameManager : Singleton<GameManager>
    {

        public GameObject loadingPrefab;
        private GameObject loadingPrefabInstance;
        private Coroutine loadSceneCoroutine;
        private string loadSceneName;

        private bool _paused = false;
        public bool Paused
        {
            get => _paused;
            set {
                _paused = value;
                if (PauseAction != null)
                    PauseAction();

                Debug.Log("Game pause = " + _paused);
                Time.timeScale = value ? 0f : 1f;
                AudioListener.pause = value;
            }
        }

        public UnityAction PauseAction;

        public void ResetGame()
        {
            WellKnownUIManager.Instance.HideUI(WellKnownUIType.DeathPanel);
            WellKnownUIManager.Instance.HideUI(WellKnownUIType.WinPanel);
        }

        private IEnumerator DoLoadScene()
        {
            string lLoadSceneName;
            do
            {
                lLoadSceneName = loadSceneName;

                var asyncLoad = SceneManager.LoadSceneAsync(lLoadSceneName);
                while (!asyncLoad.isDone)
                    yield return null;
            } while (lLoadSceneName != loadSceneName);

            Paused = false;
        }

        public void LoadIntoLevel(string level)
        {
            Paused = true;
            ResetGame();

            loadSceneName = level;
            loadingPrefabInstance = Instantiate(loadingPrefab);
            if (loadSceneCoroutine == null)
                StartCoroutine(DoLoadScene());
        }

        public void LoadMainMenu()
        {
            LoadIntoLevel("MainMenuScene");
        }

    }
}
