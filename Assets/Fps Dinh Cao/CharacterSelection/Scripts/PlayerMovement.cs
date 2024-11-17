using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.Mirror.Tutorials.CharacterSelection
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] CharacterController characterController;

        public float moveSpeed = 10f;
        //public float gravityPlayer = -9.81f;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private float mouseSensitivity = 100f;
        private float xRotation = 0f;

        //Vector3 velocity;
        public override void OnStartLocalPlayer()
        {
            if (!isLocalPlayer)
            {
                enabled = false; // Disable this script for non-local players
                return;
            }
            playerCamera =Camera.main.transform;
            playerCamera.SetParent(transform);
            playerCamera.transform.position = Vector3.zero;
            // Khóa chuột ban đầu
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        void Update()
        {
            if (!isLocalPlayer) { Debug.Log("Hello"); return; }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up, mouseX);

            Vector3 moveDirection = transform.right * x + transform.forward * z;

            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
