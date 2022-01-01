using System;
using UnityEngine;


namespace Mahou
{
    public class PlayerMovementController : MonoBehaviour
    {
        private CharacterController m_CharacterController;
        private Vector3 m_Motion = Vector3.zero;
        public Camera PlayerCamera;
        public MovementConstantData MovementConstants;

        void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            // horizontal character rotation
            if (!Input.GetKey(KeyCode.LeftAlt))
            {
                transform.Rotate(new Vector3(0f, Input.GetAxisRaw("Mouse X") * MovementConstants.RotateSpeed, 0f), Space.Self);
                float y = PlayerCamera.transform.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * MovementConstants.RotateSpeed;
                PlayerCamera.transform.localEulerAngles = new Vector3(y, 0, 0);
            }

            // movement
            if (m_CharacterController.isGrounded && m_Motion.y < 0)
                m_Motion.y = 0;
            Vector3 dir = transform.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized) * MovementConstants.MoveSpeed;
            dir.y = m_Motion.y;
            m_Motion = Vector3.Lerp(m_Motion, dir, MovementConstants.MoveResponsiveness * Time.deltaTime);
            m_Motion.y -= MovementConstants.GravityStrength * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
                m_Motion.y = MovementConstants.JumpStrength;
            m_CharacterController.Move(m_Motion);
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