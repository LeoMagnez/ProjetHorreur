using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{

    [SerializeField] private bool _enable = true; //Désactive le HeadBob si besoin

    [Header("Courbes de Lissajous")]
    [SerializeField, Range(0, 10f)] private float _amplitudeA = 1f;
    [SerializeField, Range(0, 10f)] private float _amplitudeB = 0.15f;
    [SerializeField, Range(0, 10f)] private float _frequencyA = 0.01f;
    [SerializeField, Range(0, 10f)] private float _frequencyB = 0.01f;

    [Header("Paramètres supplémentaires")]
    [SerializeField, Range(0, 0.1f)] private float _offsetAmplitude;

    [SerializeField]private Vector3 randomOffset = Vector3.zero;

    [Header("Références")]
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 2.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_enable)
        {
            return;
        }

        CheckMotion();
        _camera.LookAt(FocusTarget());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition = motion;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude; //execute le mouvement si speed est supérieur à _toggleSpeed
        ResetPosition();
        if(speed < _toggleSpeed)
        {
            return;
        }
        if (!_controller.isGrounded)
        {
            return;
        }

        PlayMotion(FootStepMotion());
    }
    private Vector3 FootStepMotion()
    {
        //_amplitude contrôle la "taille" du mouvement (possibilité d'exagerer le mouvement ou de le réduire)
        //_frequency contrôle la vitesse du mouvement (possibilité de changer pour la vitesse de sprint / etre sneaky)
        //Voir Courbes de Lissajous

        Vector3 pos = Vector3.zero;
        

        pos.x = _amplitudeA * Mathf.Sin(_frequencyA * Time.time + 0f);
        pos.y = _amplitudeB * Mathf.Sin(_frequencyB * Time.time + 0f);

        //Léger offset de la courbe de manière aléatoire 
        randomOffset += Random.insideUnitSphere * _offsetAmplitude;
        randomOffset = Vector3.ClampMagnitude(randomOffset, 0.05f);
        pos += randomOffset;

        return pos;
    }

    private void ResetPosition()
    {
        if(_camera.localPosition == _startPos)
        {
            return;
        }

        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget() //Stabilisation du mouvement
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
