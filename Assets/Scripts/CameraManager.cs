/*ETPA 2022 - 2023 // JV2.1 PROJET S4 - GROUPE 4 // CameraManager.cs
 
 Programmers - ALBOUYS Evangeline, MAGNEZ L�o, POULAIN--BONNET Matisse

 Le but de ce script est de g�rer la m�canique principale du jeu : prendre des photos de l'environnement et utiliser certaines
d'entre elles pour modifier la position de certains objets. 

/!\ Ce script est directement reli� au script "GalleryManager.cs" /!\
*/

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
            Destroy(gameObject);    // Suppression d'une instance pr�c�dente

        instance = this;
    }
    #endregion

    #region START
    private void Start()
    {
        canPlay = true; //Permet de jouer au start
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        //Si on appuie sur clic-droit, et que l'on est pas en train de naviguer la galerie, sors l'appareil photo
        if (Input.GetMouseButtonUp(1) && !_isUIup) 
        {
            if (!_isCameraUp)
            {
                CameraUp(); //l�ve l'appareil
                _lightCamera.SetActive(true); //allume la lumi�re du flash lorsqu'on sort l'appareil
            }
            else
            {
                CameraDown();//range l'appareil
                _lightCamera.SetActive(false); //d�sactive la lumi�re du flash lorsque l'appareil est rang�
            }
        }


        //Si on appuie sur Tab et que l'appareil photo est rang� alors ouvre la galerie
        if (Input.GetKeyDown(KeyCode.Tab) && !_isCameraUp)
        {
            if (!_isUIup)
            {

                _lightCamera.SetActive(false); //d�sactive la lumi�re du flash lorsqu'on est dans la galerie
                UIup();//entre dans la galerie

            }
            else
            {

                UIdown();//sort de la galerie
            }
        }

        //Si on appuie sur D et qu'on ne peut pas jouer (a.k.a, lorsqu'on est dans la galerie), permet de naviguer � l'interieur
        if (Input.GetKeyDown(KeyCode.D) && !canPlay)
        {

            galleryManager.selectNextOrPrevious(1); //navigue vers la droite


        }

        //Si on appuie sur D et qu'on ne peut pas jouer (a.k.a, lorsqu'on est dans la galerie), permet de naviguer � l'interieur
        if (Input.GetKeyDown(KeyCode.Q) && !canPlay)
        {

            galleryManager.selectNextOrPrevious(-1); //navigue vers la gauche

        }

        TakePhoto(); //V�rifie si on peut prendre une photo
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
        _isCameraUp = true; //passe ce flag en true (emp�che d'aller dans la galerie si on s'appr�te � prendre une photo)
        _camera.SetTrigger("camera_activation"); //joue l'animation o� le joueur l�ve l'appareil photo
        //UI.SetActive(true);
    }
    private void CameraDown()
    {
        _isCameraUp = false;//passe ce flag en false (autorise l'ouverture de la galerie)
        _camera.SetTrigger("camera_desactivation");//joue l'animation o� le joueur baisse l'appareil photo
        //UI.SetActive(false);
    }
    #endregion

    #region UI
    private void UIup()
    {
        //Chargement des images de gallerie
        galleryManager.OnGalleryUpdatePage();

        _isCameraUp = false;
        canPlay = false; //emp�che le joueur de bouger lorsqu'il est dans la galerie
        _isUIup = true; //emp�che le joueur de passer en mode photo lorsqu'il est dans la galerie
        _cameraUI.SetTrigger("UICameraUp"); //joue l'animation d'ouverture de la galerie
    }

    private void UIdown()
    {

        canPlay = true; //autorise le joueur � bouger lorsque la galerie est ferm�e
        _isUIup = false; //permet le joueur de passer en mode photo une fois la galerie ferm�e
        _cameraUI.SetTrigger("UICameraDown");//joue l'animation de fermeture de la galerie

    }
    #endregion

    #region TAKE_PHOTO
    private void TakePhoto()
    {
        //Si on appuie sur clic gauche avec l'appareil photo lev�, prend une photo
        if (Input.GetMouseButtonDown(0) && _isCameraUp)
        {
            _takingPhoto = true;

            Raycast(); //v�rifie si la photo prise est une "photo importante"

            SaveRenderTextureToFile.SaveRTToFile(rt, _screenNumber); //sauvegarde la photo dans une render texture et l'encode en PNG pour pouvoir aller la visionner
            Texture2D temp = Resources.Load<Texture2D>("capture" + _screenNumber); //convertit la photo en Texture2D
            Sprite _temp = Sprite.Create(SaveRenderTextureToFile.tex, new Rect(0, 0, SaveRenderTextureToFile.tex.width, SaveRenderTextureToFile.tex.height), new Vector2(0.5f, 0.5f)); //transforme la photo en sprite pour l'ajouter � la galerie
            //_takenPictures.Add(SaveRenderTextureToFile.tex);
            _spriteList.Add(_temp); //ajoute la photo � la liste
            _screenNumber++; //incr�mente le nom de la photo pour pouvoir en prendre � l'infini

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
                Debug.Log("Je d�tecte quelque chose d'important");

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
