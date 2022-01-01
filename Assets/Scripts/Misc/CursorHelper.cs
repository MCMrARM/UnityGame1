using UnityEngine;

namespace Mahou
{

    public class CursorHelper : MonoBehaviour
    {
        void Update()
        {
            Cursor.lockState = Input.GetKey(KeyCode.LeftAlt) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

}