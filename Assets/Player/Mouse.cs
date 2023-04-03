using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mouse : MonoBehaviour
{
    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    private float mouseX;
    private float mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    private float xRotation = 0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void RecieveInput(Vector2 MouseInput) 
    {
        mouseX = MouseInput.x * sensitivityX;
        mouseY = MouseInput.y * sensitivityY;
    }
}
