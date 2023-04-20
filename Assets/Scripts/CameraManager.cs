/*ETPA 2022 - 2023 // JV2.1 PROJET S4 - GROUPE 4 // CameraManager.cs
 
 Programmers - ALBOUYS Evangeline, MAGNEZ Léo, POULAIN--BONNET Matisse

 Le but de ce script est de gérer la mécanique principale du jeu : prendre des photos de l'environnement et utiliser certaines
d'entre elles pour modifier la position de certains objets. 

/!\ Ce script est directement relié au script "GalleryManager.cs" /!\
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

    [SerializeField] bool _importantPhoto = false;

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
    //public List<Texture2D> _takenPictures = new List<Texture2D>();
    public List<Sprite> _spriteList = new List<Sprite>();
    public List<ObjectMover> _objectToMoveList = new List<ObjectMover>();
    public ObjectMover objectToMove;
    //public List<MeshRenderer> _photoPlanes = new List<MeshRenderer>();


    [Header("Render Textures")]
    public RenderTexture rt;


    [Header("Animators")]
    public Animator _camera;

    public Animator _cameraUI;

    public bool _objetImportantGallery = false;

    public int _imgImportanteIndex = 0;

    [Header("References")]
    [SerializeField] GalleryManager galleryManager;

    [Header("Sound")]
    [SerializeField] GameObject SFX;
    [SerializeField] private AK.Wwise.Event cameraShutterSFX;

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
        canPlay = true; //Permet de jouer au start
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        //Si on appuie sur clic-droit, et que l'on est pas en train de naviguer la galerie, sors l'appareil photo
        if (Input.GetMouseButtonUp(1) && !_isUIup && !MenuManager.instance._menuIsUp ) 
        {
            if (!_isCameraUp)
            {
                CameraUp(); //lève l'appareil
            }
            else
            {
                CameraDown();//range l'appareil
            }
        }


        //Si on appuie sur Tab et que l'appareil photo est rangé alors ouvre la galerie
        if (Input.GetKeyDown(KeyCode.Tab) && !_isCameraUp && !MenuManager.instance._menuIsUp)
        {
            if (!_isUIup)
            {

                _lightCamera.SetActive(false); //désactive la lumière du flash lorsqu'on est dans la galerie
                UIup();//entre dans la galerie

            }
            else
            {

                UIdown();//sort de la galerie
            }
        }

        //Si on appuie sur D et qu'on ne peut pas jouer (a.k.a, lorsqu'on est dans la galerie), permet de naviguer à l'interieur
        if (Input.GetKeyDown(KeyCode.D) && !canPlay)
        {

            galleryManager.selectNextOrPrevious(1); //navigue vers la droite


        }

        //Si on appuie sur D et qu'on ne peut pas jouer (a.k.a, lorsqu'on est dans la galerie), permet de naviguer à l'interieur
        if (Input.GetKeyDown(KeyCode.Q) && !canPlay)
        {

            galleryManager.selectNextOrPrevious(-1); //navigue vers la gauche

        }

        TakePhoto(); //Vérifie si on peut prendre une photo
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
        _isCameraUp = true; //passe ce flag en true (empêche d'aller dans la galerie si on s'apprête à prendre une photo)
        _camera.SetTrigger("camera_activation"); //joue l'animation où le joueur lève l'appareil photo
        //UI.SetActive(true);
    }
    private void CameraDown()
    {
        _isCameraUp = false;//passe ce flag en false (autorise l'ouverture de la galerie)
        _camera.SetTrigger("camera_desactivation");//joue l'animation où le joueur baisse l'appareil photo
        //UI.SetActive(false);
    }
    #endregion

    #region UI
    private void UIup()
    {
        //Chargement des images de gallerie
        galleryManager.OnGalleryUpdatePage();

        _isCameraUp = false;

        canPlay = false; //empêche le joueur de bouger lorsqu'il est dans la galerie
        _isUIup = true; //empêche le joueur de passer en mode photo lorsqu'il est dans la galerie
        _cameraUI.SetTrigger("UICameraUp"); //joue l'animation d'ouverture de la galerie
    }

    public void UIdown()
    {
        
        canPlay = true; //autorise le joueur à bouger lorsque la galerie est fermée
        _isUIup = false; //permet le joueur de passer en mode photo une fois la galerie fermée
        _cameraUI.SetTrigger("UICameraDown");//joue l'animation de fermeture de la galerie
        

    }
    #endregion

    #region TAKE_PHOTO
    private void TakePhoto()
    {
        //Si on appuie sur clic gauche avec l'appareil photo levé, prend une photo
        if (Input.GetMouseButtonDown(0) && _isCameraUp && objectToMove == null)
        {
            _lightCamera.SetActive(true); //allume la lumière du flash lorsqu'on prend une photo
            _takingPhoto = true;

            

            StartCoroutine(WaitForFlash());

            _screenNumber++; //incrémente le nom de la photo pour pouvoir en prendre à l'infini
        }

    }
    #endregion

    #region IMPORTANT_PHOTO_DETECTION_RAYCAST
    public void Raycast()
    {
        RaycastHit hit; //Lancement du raycast

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 10f))
        {
            
            if (hit.transform.tag == "_photoImportante" && _takingPhoto) //Lancement d'une condition sur le raycast touche un item avec le tag "_photoImportante"
            {
                _importantPhoto = true;
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Je détecte quelque chose d'important");
                _objectToMoveList.Insert(0, null);
                _takingPhoto = false;
                GameOjectManager.instance._listeObj[12].SetActive(false); //Fait disparaitre la porte quand elle est prise en photo  
                GameOjectManager.instance._listeObj[10].SetActive(true); //Fait apparaitre le trigger de changement de niveau quand la porte est prise en photo

                //_objetImportant.SetActive(false); //Désactivation du GameOject spécifié

                //_objetImportantGallery = true; //Active une bool qui indique qu'une photo importante a été prise
                //_porte.SetActive(false); //Désactivation du GameOject spécifié

            }

            else if (hit.transform.tag == "_photoNormale" && _takingPhoto)
            {
                hit.transform.gameObject.GetComponent<ObjectMover>().HideObject();
                //_objectToMoveList.Insert(0, hit.transform.gameObject.GetComponent<ObjectMover>());
                objectToMove = hit.transform.gameObject.GetComponent<ObjectMover>();

                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
            else
            {
               // _objectToMoveList.Insert(0, null);
            }
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            //_objectToMoveList.Insert(0, null);
            if (_takingPhoto)
            {
                _takingPhoto = false;
            }
        }
    }

    public IEnumerator WaitBeforeDisablingCamera()
    {
        yield return new WaitForSeconds(1.5f);
        _cameraUIParent.SetActive(false);
    }

    private IEnumerator WaitForFlash()
    {
        yield return new WaitForSeconds(0.01f);

        Raycast(); //vérifie si la photo prise est une "photo importante"

        Sprite _temp = SaveRenderTextureToFile.ToTexture2D(rt, _screenNumber); //sauvegarde la photo dans une render texture et l'encode en PNG pour pouvoir aller la visionner

        if (_importantPhoto)
        {
            _spriteList.Insert(0, _temp); //si une photo est importante, l'ajoute en haut de la galerie
            _imgImportanteIndex = 0;
            _importantPhoto = false;
        }
        else
        {
            _spriteList.Insert(0, _temp); //ajoute la photo à la liste
            _imgImportanteIndex++;
        }

        _lightCamera.SetActive(false);
        cameraShutterSFX.Post(SFX);
        _objetImportant.SetActive(false); //Désactivation de l'objet important
    }
    #endregion


    private void OnApplicationQuit()
    {
        SaveRenderTextureToFile.WritePhotoToFile(_spriteList.ToArray());
    }

    #endregion

}
