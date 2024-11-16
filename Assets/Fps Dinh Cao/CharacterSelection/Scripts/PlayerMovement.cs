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

        //Vector3 velocity;

        public void Update()
        {
            if(!isOwned){return;}
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.right * x + transform.forward * z;

            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
           // velocity.y += gravityPlayer * Time.deltaTime;
            //characterController.Move(velocity * Time.deltaTime);
        }
    }
}
