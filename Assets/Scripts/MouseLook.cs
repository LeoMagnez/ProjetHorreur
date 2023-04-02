using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    float mouseX;
    float mouseY;
    float xRotation;

    [Header("Values")]
    public float mouseSensitivity = 100f;

    [Header("References")]
    public Transform player;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraManager.instance.canPlay)
        {
            move();
        }
        

    }

    void move()
    {
        mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, -0f);
        player.Rotate(Vector3.up * mouseY);
    }
}
