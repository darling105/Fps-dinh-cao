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
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private float mouseSensitivity = 100f;
        private float xRotation = 0f;
        [SyncVar(hook = nameof(TakeDame))]
        public float Health = 100f;

        //Vector3 velocity;
        public override void OnStartLocalPlayer()
        {
            if (!isLocalPlayer)
            {
                enabled = false; // Disable this script for non-local players
                return;
            }
            playerCamera.SetActive(true);
            
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
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up, mouseX);

            Vector3 moveDirection = transform.right * x + transform.forward * z;

            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
            {
                CmdPlayMuzzleFlash();
                RaycastAttack();
            }
        }
        [ClientCallback]
        private void RaycastAttack()
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 1000f))
            {
                PlayerMovement other = hit.transform.GetComponent<PlayerMovement>();
                if(other != null){
                    CmdAttack(other.gameObject);
                }
            }

        }
        [Command]
        private void CmdAttack(GameObject target){
            target.GetComponent<PlayerMovement>().Health -= 10;
        }
        void TakeDame(float _new,float old){
            Debug.Log("my name is:" + gameObject.name);
        }
        [Command]
        void SetHealth(){
            Health =100f;
        }
        [Command]
        void CmdPlayMuzzleFlash(){
            PlayMuzzleFlash();
        }
        [ClientRpc]
        void PlayMuzzleFlash(){
            //play muzzleflash
        }
    }
}
