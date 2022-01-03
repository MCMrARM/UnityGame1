﻿using UnityEngine;

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
            get => !InMenu && !GameManager.Instance.Paused;
        }

        public bool ShouldLockCursor
        {
            get => CanMovePlayer;
        }


        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
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
            return Input.GetButton("Jump") || Input.GetButtonDown("Jump");
        }

    }
}