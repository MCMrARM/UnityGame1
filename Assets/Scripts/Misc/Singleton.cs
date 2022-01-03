using UnityEngine;

namespace Mahou
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T _instance;

        public static T Instance
        {
            get {
                if (_instance?.gameObject == null)
                    _instance = null;
                if (_instance == null)
                    _instance = FindObjectOfType<T>();
                return _instance;
            }
        }

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Debug.LogError("An instance of the singleton " + GetType().Name + " already exists.");
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

    }
}
