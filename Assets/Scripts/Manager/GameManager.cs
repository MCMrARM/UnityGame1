using UnityEngine;
using UnityEngine.Events;

namespace Mahou
{
    class GameManager : Singleton<GameManager>
    {

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

    }
}
