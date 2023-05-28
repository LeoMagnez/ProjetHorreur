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
using UnityEngine.VFX;

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

    public bool unstableTable;

    public bool unstableChair;

    public bool unstableLamp;

    public bool plank1, plank2, plank3, plank4;

    [SerializeField] bool _importantPhoto = false;

    [Header("GameObjects")]

    public GameObject UI;

    public GameObject _porte;

    public GameObject _lightCamera;

    [SerializeField] GameObject _cameraUIParent;
    [SerializeField] GameObject photoTuto;

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

    [SerializeField] GameObject startOfGameCanvas;
    [SerializeField] Animator startOfGameAnimator;
    [SerializeField] GameObject sparks;
    [SerializeField] CamShake shake;
    [SerializeField] GameObject cinemachineCam;
    [SerializeField] GameObject maison, maison_exterieur, telephone, poubelles, poubelles_ghost, slidingWall, concrete_disp;

    [Header("Sound")]
    [SerializeField] GameObject SFX;
    [SerializeField] private AK.Wwise.Event cameraShutterSFX;
    [SerializeField] private AK.Wwise.Event forbiddenCameraSFX;

    public bool cameraTuto = false;
    public bool firstTimeOpeningGallery;

    public GameObject CameraJoueur;


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

        if(photoTuto != null)
        {
            photoTuto.SetActive(true);
        }
 
        

    }
    #endregion

    #region UPDATE
    private void Update()
    {

        if (cameraTuto)
        {

            //Si on appuie sur clic-droit, et que l'on est pas en train de naviguer la galerie, sors l'appareil photo
            if (Input.GetMouseButtonUp(1) && !_isUIup && !MenuManager.instance._menuIsUp)
            {
                if (!_isCameraUp)
                {
                    CameraUp(); //l�ve l'appareil
                }
                else
                {
                    CameraDown();//range l'appareil
                }
            }

            //Si on appuie sur Tab et que l'appareil photo est rang� alors ouvre la galerie
            if (Input.GetKeyDown(KeyCode.Tab) && !_isCameraUp && !MenuManager.instance._menuIsUp)
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

        if(!firstTimeOpeningGallery && photoTuto != null)
        {
            StartCoroutine(WaitForHouse());
        }

        _isCameraUp = false;

       
        _isUIup = true; //emp�che le joueur de passer en mode photo lorsqu'il est dans la galerie
        _cameraUI.SetTrigger("UICameraUp"); //joue l'animation d'ouverture de la galerie
        canPlay = false; //emp�che le joueur de bouger lorsqu'il est dans la galerie
    }

    public void UIdown()
    {
        if (!firstTimeOpeningGallery && photoTuto != null)
        {

            photoTuto.SetActive(false);
            firstTimeOpeningGallery = true;
            
        }

        _isUIup = false; //permet le joueur de passer en mode photo une fois la galerie ferm�e
        _cameraUI.SetTrigger("UICameraDown");//joue l'animation de fermeture de la galerie
        canPlay = true; //autorise le joueur � bouger lorsque la galerie est ferm�e


    }

    public IEnumerator WaitForHouse()
    {
        yield return new WaitForSeconds(0.5f);
        maison.SetActive(true);
        maison_exterieur.SetActive(true);
        poubelles.SetActive(true);
        slidingWall.SetActive(true);
        poubelles_ghost.SetActive(true);
        telephone.SetActive(true);
        concrete_disp.SetActive(false);
    }
    #endregion

    #region TAKE_PHOTO
    private void TakePhoto()
    {
        //Si on appuie sur clic gauche avec l'appareil photo lev�, prend une photo
        if (Input.GetMouseButtonDown(0) && _isCameraUp && objectToMove == null)
        {
            _lightCamera.SetActive(true); //allume la lumi�re du flash lorsqu'on prend une photo
            _takingPhoto = true;

            StartCoroutine(WaitForFlash());

            _screenNumber++; //incr�mente le nom de la photo pour pouvoir en prendre � l'infini
        }
        else if (Input.GetMouseButtonDown(0) && _isCameraUp && objectToMove != null)
        {
            StartCoroutine(shake.Shake(0.1f, 0.2f));
            StartCoroutine(ForbiddenPhoto());
            forbiddenCameraSFX.Post(SFX);
        }
    }
    #endregion

    #region IMPORTANT_PHOTO_DETECTION_RAYCAST
    public void Raycast()
    {
        RaycastHit hit; //Lancement du raycast
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 20, Color.red, 1f);
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 20f))
        {
            Debug.Log(hit.transform.gameObject.name); 
            if (hit.transform.tag == "_photoImportante" && _takingPhoto) //Lancement d'une condition sur le raycast touche un item avec le tag "_photoImportante"
            {
                _importantPhoto = true;
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Je d�tecte quelque chose d'important");
                _objectToMoveList.Insert(0, null);
                _takingPhoto = false;
                GameOjectManager.instance._listeObj[12].SetActive(false); //Fait disparaitre la porte quand elle est prise en photo  
                GameOjectManager.instance._listeObj[10].SetActive(true); //Fait apparaitre le trigger de changement de niveau quand la porte est prise en photo

                //_objetImportant.SetActive(false); //D�sactivation du GameOject sp�cifi�

                //_objetImportantGallery = true; //Active une bool qui indique qu'une photo importante a �t� prise
                //_porte.SetActive(false); //D�sactivation du GameOject sp�cifi�

            }

            else if (hit.transform.tag == "_photoNormale" && _takingPhoto)
            {
                
                hit.transform.gameObject.GetComponent<ObjectMover>().HideObject();
                //_objectToMoveList.Insert(0, hit.transform.gameObject.GetComponent<ObjectMover>());
                objectToMove = hit.transform.gameObject.GetComponent<ObjectMover>();

                if(hit.transform.gameObject.name == "UnstableTable")
                {
                    unstableTable = true;
                    unstableChair = false;
                    unstableLamp = false;
                }

                if(hit.transform.gameObject.name == "UnstableChair")
                {
                    unstableChair = true;
                    unstableLamp = false;
                    unstableTable = false;
                }

                if(hit.transform.gameObject.name == "UnstableLamp")
                {
                    unstableLamp = true;
                    unstableChair = false;
                    unstableTable = false;
                }

                if (hit.transform.gameObject.name == "Plank1")
                {
                    plank1 = true;
                    plank2 = false;
                    plank3 = false;
                    plank4 = false;
                }

                if (hit.transform.gameObject.name == "Plank2")
                {
                    plank1 = false;
                    plank2 = true;
                    plank3 = false;
                    plank4 = false;
                }

                if (hit.transform.gameObject.name == "Plank3")
                {
                    plank1 = false;
                    plank2 = false;
                    plank3 = true;
                    plank4 = false;
                }

                if (hit.transform.gameObject.name == "Plank4")
                {
                    plank1 = false;
                    plank2 = false;
                    plank3 = false;
                    plank4 = true;
                }

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

        Raycast(); //v�rifie si la photo prise est une "photo importante"

        Sprite _temp = SaveRenderTextureToFile.ToTexture2D(rt, _screenNumber); //sauvegarde la photo dans une render texture et l'encode en PNG pour pouvoir aller la visionner

        if (_importantPhoto)
        {
            _spriteList.Insert(0, _temp); //si une photo est importante, l'ajoute en haut de la galerie
            _imgImportanteIndex = 0;
            _importantPhoto = false;
        }
        else
        {
            _spriteList.Insert(0, _temp); //ajoute la photo � la liste
            _imgImportanteIndex++;
        }

        _lightCamera.SetActive(false);
        cameraShutterSFX.Post(SFX);

    }
    #endregion


    private void OnApplicationQuit()
    {
        SaveRenderTextureToFile.WritePhotoToFile(_spriteList.ToArray());
    }

    public IEnumerator ForbiddenPhoto()
    {
        sparks.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sparks.SetActive(false);
    }
    #endregion



}
