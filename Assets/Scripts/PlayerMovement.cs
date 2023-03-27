using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MoveSettings[] _settings = null;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private GameObject _spray;

    private Vector3 _moveDirection;
    public CharacterController _controller;

    [Header("Idle cam values")]
    public float idleCamNoiseAmplitude;
    public float idleCamNoiseFrequency;

    [Header("Walking cam values")]
    public float walkingCamNoiseAmplitude;
    public float walkingCamNoiseFrequency;

    [Header("Running cam values")]
    public float runningCamNoiseAmplitude;
    public float runningCamNoiseFrequency;

    private bool isIdle = true;
    private bool isRunnning = false;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        //player is idle on awake
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = idleCamNoiseAmplitude;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = idleCamNoiseFrequency;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKey(KeyCode.H))
        {
            isRunnning = true;
            RunningMovement();
        }
        else
        {
            isRunnning = false;
            DefaultMovement();
        }
        //Spray();
        CamMouvement();

        
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void DefaultMovement()
    {
        if (_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //inputs

            if(input.x != 0 && input.y != 0)
            {
                input *= 0.777f; //Normalizes Vector2 "input". Prevent the player from being faster if walking diagonally
            }

            if(input.x != 0 || input.y != 0)
            {
                isIdle = false;
            }
            else
            {
                isIdle = true;
            }

            _moveDirection.x = input.x * _settings[0].speed;
            _moveDirection.z = input.y * _settings[0].speed;
            _moveDirection.y = -_settings[0].speed;

            _moveDirection = transform.TransformDirection(_moveDirection);

        }
        else
        {
            _moveDirection.y -= _settings[0].gravity * Time.deltaTime;
        }
    }

    private void RunningMovement()
    {
        if (_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //inputs

            if (input.x != 0 && input.y != 0)
            {
                input *= 0.777f; //Normalizes Vector2 "input". Prevent the player from being faster if walking diagonally
            }

            _moveDirection.x = input.x * _settings[1].speed;
            _moveDirection.z = input.y * _settings[1].speed;
            _moveDirection.y = -_settings[1].speed;

            _moveDirection = transform.TransformDirection(_moveDirection);


        }
        else
        {
            _moveDirection.y -= _settings[1].gravity * Time.deltaTime;
        }
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

    public void CamMouvement() //Change the cinemachine virtual camera's noise according to the player's sate (idle, walking, running)
    {
        if(!isIdle && !isRunnning)
        {
            //adjust frequency and amplitude when the player is walking
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = walkingCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = walkingCamNoiseFrequency;
        }
        else if (isRunnning && !isIdle)
        {

            //adjust frequency and amplitude when the player is running
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = runningCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = runningCamNoiseFrequency;
        }
        else
        {
            //adjust frequency and amplitude when the player is idle
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = idleCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = idleCamNoiseFrequency;
        }
    }


}
