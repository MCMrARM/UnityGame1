using UnityEngine;

namespace Mahou
{
    public class InputManager : Singleton<InputManager>
    {

        private int _inMenuCount = 0;

        public bool InMenu
        {
            get => _inMenuCount > 0;
        }

        public bool CanMovePlayer
        {
            get => !InMenu && !GameManager.Instance.Paused && !Input.GetKey(KeyCode.LeftAlt);
        }

        public bool ShouldLockCursor
        {
            get => CanMovePlayer;
        }


        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && !InMenu)
                GameManager.Instance.Paused = !GameManager.Instance.Paused;

            Cursor.lockState = ShouldLockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void OnOpenMenu()
        {
            _inMenuCount++;
        }

        public void OnCloseMenu()
        {
            _inMenuCount--;
        }

        public Vector2 GetLookInput()
        {
            if (!CanMovePlayer)
                return Vector2.zero;
            return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

        public Vector2 GetMoveInput()
        {
            if (!CanMovePlayer)
                return Vector2.zero;
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        public bool GetJumpInput()
        {
            return CanMovePlayer && (Input.GetButton("Jump") || Input.GetButtonDown("Jump"));
        }

        public bool GetFireInput()
        {
            return CanMovePlayer && Input.GetButton("Fire1");
        }

        public int GetSpellSwitchInput()
        {
            if (Input.GetButtonDown("Spell1"))
                return 1;
            if (Input.GetButtonDown("Spell2"))
                return 2;
            if (Input.GetButtonDown("Spell3"))
                return 3;
            if (Input.GetButtonDown("Spell4"))
                return 4;
            return -1;
        }

    }
}
