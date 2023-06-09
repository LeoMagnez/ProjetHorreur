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

    [Header("Holding Camera cam values")]
    public float holdingCamNoiseAmplitude;
    public float holdingCamNoiseFrequency;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunnning = true;
            RunningMovement();
        }
        else
        {
            isRunnning = false;
            DefaultMovement();
        }

        if (CameraManager.instance._isCameraUp)
        {
            HoldingCamMovement();
        }

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

    private void HoldingCamMovement()
    {
        if (_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //inputs

            if (input.x != 0 && input.y != 0)
            {
                input *= 0.777f; //Normalizes Vector2 "input". Prevent the player from being faster if walking diagonally
            }

            _moveDirection.x = input.x * _settings[2].speed;
            _moveDirection.z = input.y * _settings[2].speed;
            _moveDirection.y = -_settings[2].speed;

            _moveDirection = transform.TransformDirection(_moveDirection);


        }
        else
        {
            _moveDirection.y -= _settings[2].gravity * Time.deltaTime;
        }
    }


    public void CamMouvement() //Change the cinemachine virtual camera's noise according to the player's sate (idle, walking, running)
    {
        if(!isIdle && !isRunnning &&!CameraManager.instance._isCameraUp)
        {
            //adjust frequency and amplitude when the player is walking
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = walkingCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = walkingCamNoiseFrequency;
        }
        else if (isRunnning && !isIdle && !CameraManager.instance._isCameraUp)
        {


            //adjust frequency and amplitude when the player is running
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = runningCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = runningCamNoiseFrequency;
        }

        else if (CameraManager.instance._isCameraUp)
        {

            //adjust frequency and amplitude when the player is holding the camera
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = holdingCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = holdingCamNoiseFrequency;
        }

        else
        {
            //adjust frequency and amplitude when the player is idle
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = idleCamNoiseAmplitude;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = idleCamNoiseFrequency;
        }
    }


}
