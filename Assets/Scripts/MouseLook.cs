using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    float mouseX;
    float mouseY;
    float xRotation;

    [Header("Values")]
    public static float mouseSensitivity = 100f;

    [Header("References")]
    public Transform player;





    // Start is called before the first frame update
    void Start()
    {
       mouseSensitivity = 300f;
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (CameraManager.instance.canPlay)
        {
            move();
        }*/

        if (MenuManager.instance._menuIsUp == false)
        {
            if (MenuManager.instance.InvertAxes == true)
            {
                Debug.Log("Axes inversé");
                invertMove();
            }
            else
            {
                Debug.Log("Axes normaux");
                move();
            }
        }

        if(MenuManager.instance._menuIsUp == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (CameraManager.instance.canPlay)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

    public void invertMove()
    {

            mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            xRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, -0f);
            player.Rotate(Vector3.up * mouseY);


    }

   

}
