using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 3f;
    Vector2 horizontalInput;

    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] LayerMask groundMask;
    private bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded) 
        {
            verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
    public void RecieveInput(Vector2 _horiontalInput) 
    {
        horizontalInput = _horiontalInput;
        Debug.Log(horizontalInput);
    }
}
