using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour

{
    #region SINGLETON
    public static CameraManager instance { get; private set; }
    #endregion

    #region VARIABLES
    private int _screenNumber = 0;
    int pageNumber = 0;

    [Header("Flags")]
    public bool _isCameraUp = false;

    public bool _isUIup = false;

    public bool _takingPhoto = false;

    public bool canPlay;

    [Header("GameObjects")]

    public GameObject UI;

    public GameObject _porte;

    public GameObject _objetImportant;

    public GameObject _lightCamera;

    [SerializeField] GameObject _cameraUIParent;

    public MeshRenderer MeshRenderer;

    [Header("Materials & Textures")]
    public Material _whiteMaterial;
    public Material _blackMaterial;
    public Texture image;
    public Texture2D test;

    [Header("Lists")]
    public List<Texture2D> _takenPictures = new List<Texture2D>();
    public List<Sprite> _spriteList = new List<Sprite>();
    //public List<MeshRenderer> _photoPlanes = new List<MeshRenderer>();


    [Header("Render Textures")]
    public RenderTexture rt;


    [Header("Animators")]
    public Animator _camera;

    public Animator _cameraUI;





    


    [Header("References")]
    [SerializeField] GalleryManager galleryManager;

    #endregion


    #region FUNCTIONS


    #region AWAKE
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente

        instance = this;
    }
    #endregion

    #region START
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
    #endregion

    #region UPDATE
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

        if (Input.GetKeyDown(KeyCode.Tab) && !_isCameraUp)
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

        if (Input.GetKeyDown(KeyCode.D) && !canPlay)
        {

            galleryManager.selectNextOrPrevious(1);


        }

       if (Input.GetKeyDown(KeyCode.Q) && !canPlay)
       {

            galleryManager.selectNextOrPrevious(-1);

       }



            //CameraUp();
            //ChangeMaterial();
            TakePhoto();
    }
    #endregion

    #region FIXED_UPDATE
    private void FixedUpdate()
    {
        
    }
    #endregion

    #region CAMERA_OBJECT
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
    #endregion

    #region UI
    private void UIup()
    {
        //Chargement des images de gallerie
        galleryManager.OnGalleryUpdatePage();

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
    #endregion

    #region TAKE_PHOTO
    private void TakePhoto()
    {
        //Possibilité de mettre une condition pour limiter le nombre de prise de photo avec _screenNumber
        if (Input.GetMouseButtonDown(0) && _isCameraUp)
        {
            _takingPhoto = true;

            Raycast();

            SaveRenderTextureToFile.SaveRTToFile(rt, _screenNumber);
            Texture2D temp = Resources.Load<Texture2D>("capture" + _screenNumber);
            Sprite _temp = Sprite.Create(SaveRenderTextureToFile.tex, new Rect(0, 0, SaveRenderTextureToFile.tex.width, SaveRenderTextureToFile.tex.height), new Vector2(0.5f, 0.5f));
            //_takenPictures.Add(SaveRenderTextureToFile.tex);
            _spriteList.Add(_temp);
            _screenNumber++;

        }

    }
    #endregion

    #region IMPORTANT_PHOTO_DETECTION
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
    #endregion
    
    
    #endregion

}
