using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MoveSettings _settings = null;
    [SerializeField] private GameObject _spray;

    private Vector3 _moveDirection;
    public CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        DefaultMovement();
        Spray();
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void DefaultMovement()
    {
        if (_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //stockage des inputs

            if(input.x != 0 && input.y != 0)
            {
                input *= 0.777f; //Normalise le Vector2 "input". Empêche de bouger plus vite en allant en diagonale
            }

            _moveDirection.x = input.x * _settings.speed;
            _moveDirection.z = input.y * _settings.speed;
            _moveDirection.y = -_settings.speed;

            _moveDirection = transform.TransformDirection(_moveDirection);

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
        }
    }

    private void Jump()
    {
        _moveDirection.y += _settings.jumpForce;
    }

    public void Spray()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 2f))
            {
                Instantiate(_spray, hit.point + Camera.main.transform.TransformDirection(Vector3.forward) * 0f, Camera.main.transform.rotation);
                
                Debug.Log("hit");
            }


        }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 2f, Color.red);
    }
}
