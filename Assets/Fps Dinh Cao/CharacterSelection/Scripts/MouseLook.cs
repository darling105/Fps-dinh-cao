using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitive = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private bool isCursorLocked = true; // Biến lưu trạng thái khóa chuột

    void Start()
    {
        // Khóa chuột ban đầu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Kiểm tra phím bật/tắt khóa chuột (Escape hoặc P)
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            ToggleCursorLock();
        }

        // Nếu chuột không bị khóa, dừng xử lý camera
        if (!isCursorLocked)
        {
            return;
        }

        // Xử lý chuyển động camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up, mouseX);
    }

    // Hàm bật/tắt trạng thái khóa chuột
    private void ToggleCursorLock()
    {
        isCursorLocked = !isCursorLocked;

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
