using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour

{
    public static CameraManager instance { get; private set; }
    private int _screenNumber = 0;


    public bool _isCameraUp = false;

    public bool _isUIup = false;

    public bool _takingPhoto = false;

    public GameObject UI;

    public MeshRenderer MeshRenderer;
    public Material _whiteMaterial;
    public Material _blackMaterial;
    public Texture image;

    public List<Texture2D> _takenPictures = new List<Texture2D>();
    public List<MeshRenderer> _photoPlanes = new List<MeshRenderer>();
    public Texture2D test;

    public RenderTexture rt;

    public GameObject _porte;

    public GameObject _objetImportant;

    public Animator _camera;

    public Animator _cameraUI;

    public GameObject _lightCamera;

    public bool canPlay;

    int planePlacement = 3;
    


    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente

        instance = this;
    }
    private void Start()
    {
        canPlay = true;
        /*if(test != null)
        {
            Debug.Log("J'ai un truc");
        }
        else
        {
            Debug.Log("J'ai rien trouvé");
        }*/

        test = Resources.Load<Texture2D>("capture0");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1) && !_isUIup) 
        {
            if (!_isCameraUp)
            {
                CameraUp();
                _lightCamera.SetActive(true);
            }
            else
            {
                CameraDown();
                _lightCamera.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !_isCameraUp)
        {
            if (!_isUIup)
            {

                _lightCamera.SetActive(false);
                UIup();
            }
            else
            {

                UIdown();
            }
        }
        //CameraUp();
        //ChangeMaterial();
        TakePhoto();
    }

    private void FixedUpdate()
    {
        
    }

    private void CameraUp()
    {
        _isCameraUp = true;
        _camera.SetTrigger("camera_activation");
        //UI.SetActive(true);
    }
    private void CameraDown()
    {
        _isCameraUp = false;
        _camera.SetTrigger("camera_desactivation");
        //UI.SetActive(false);
    }

    private void UIup()
    {
        _isCameraUp = false;
        canPlay = false;
        _isUIup = true;
        _cameraUI.SetTrigger("UICameraUp");

    }

    private void UIdown()
    {
        
        canPlay = true;
        _isUIup = false;
        _cameraUI.SetTrigger("UICameraDown");

    }

    private void TakePhoto()
    {
        //Possibilité de mettre une condition pour limiter le nombre de prise de photo avec _screenNumber
        if (Input.GetMouseButtonDown(0) && _isCameraUp)
        {
            _takingPhoto = true;
            Raycast();

            SaveRenderTextureToFile.SaveRTToFile(rt, _screenNumber);
            Texture2D temp = Resources.Load<Texture2D>("capture" + _screenNumber);
            _takenPictures.Add(SaveRenderTextureToFile.tex);
            _screenNumber++;

            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane); //Crée une plane
            plane.transform.position = new Vector3(planePlacement, 3, 0); //Set la position de la plane
            plane.transform.localScale = new Vector3(1.8f, 1, 1);
            _photoPlanes.Add(plane.GetComponent<MeshRenderer>()); //Ajoute le Mesh Renderer de chaque plane dans une liste

            planePlacement = planePlacement + 20; //Incrémente la position de chaque nouvelle plane

            for(int i = 0; i < _takenPictures.Count; i++) //attribue les photos aux materials des planes
            {
                _photoPlanes[i].material.SetTexture("_BaseColorMap", _takenPictures[i]);
            }


        }

    }
    /*private void ChangeMaterial()
    {
        MeshRenderer.material.SetTexture("_BaseMap", Resources.Load<Texture2D>("capture" + _screenNumber));
    }*/

    public void Raycast()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 50f))
        {
            
            //Debug.Log("Did Hit");
            if (hit.transform.tag == "_photoImportante" && _takingPhoto)
            {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Je détecte quelque chose d'important");

                //MeshRenderer.GetComponent<MeshRenderer>().material = _blackMaterial;
                _takingPhoto = false;
                _objetImportant.SetActive(false);
                _porte.SetActive(false);
            }

            else
            {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did not Hit");
            if (_takingPhoto)
            {
                //MeshRenderer.GetComponent<MeshRenderer>().material = _whiteMaterial;
                _takingPhoto = false;
            }
        }
    }


}
