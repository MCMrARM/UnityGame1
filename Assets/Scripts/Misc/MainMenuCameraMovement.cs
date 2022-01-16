using UnityEngine;

namespace Mahou
{

    public class MainMenuCameraMovement : MonoBehaviour
    {
        private float _startZ;
        public float resetDistance;
        public float speed;

        private void Start()
        {
            _startZ = transform.position.z;
        }

        void Update()
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            while (transform.position.z > _startZ + resetDistance)
                transform.Translate(0, 0, -resetDistance);
        }
    }

}