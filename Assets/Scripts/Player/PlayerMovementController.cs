using System;
using UnityEngine;


namespace Mahou
{
    public class PlayerMovementController : MonoBehaviour
    {
        private InputManager _inputManager;
        private CharacterController m_CharacterController;
        private Vector3 m_Motion = Vector3.zero;
        public Camera PlayerCamera;
        public MovementConstantData MovementConstants;

        void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            _inputManager = InputManager.Instance;
        }

        void Update()
        {
            if (GameManager.Instance.Paused)
                return;

            // horizontal character rotation
            var lookInput = _inputManager.GetLookInput();
            transform.Rotate(new Vector3(0f, lookInput.x * MovementConstants.RotateSpeed, 0f), Space.Self);
            float y = PlayerCamera.transform.localEulerAngles.x - lookInput.y * MovementConstants.RotateSpeed;
            PlayerCamera.transform.localEulerAngles = new Vector3(y, 0, 0);

            // movement
            if (m_CharacterController.isGrounded && m_Motion.y < 0)
                m_Motion.y = 0;
            var moveInput = _inputManager.GetMoveInput();
            Vector3 dir = transform.TransformVector(new Vector3(moveInput.x, 0, moveInput.y).normalized) * MovementConstants.MoveSpeed;
            dir.y = m_Motion.y;
            m_Motion = Vector3.Lerp(m_Motion, dir, MovementConstants.MoveResponsiveness * Time.deltaTime);
            m_Motion.y -= MovementConstants.GravityStrength * Time.deltaTime;
            if (_inputManager.GetJumpInput() && m_CharacterController.isGrounded)
                m_Motion.y = MovementConstants.JumpStrength;
            m_CharacterController.Move(m_Motion * Time.deltaTime);
        }


        [Serializable]
        public struct MovementConstantData
        {
            public float RotateSpeed;
            public float MoveSpeed;
            public float MoveResponsiveness;
            public float GravityStrength;
            public float JumpStrength;
        }
    }

}