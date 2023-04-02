/*ETPA 2022 - 2023 // JV2.1 PROJET S4 - GROUPE 4 // GalleryManager.cs
 
 Programmers - ALBOUYS Evangeline, MAGNEZ L�o, POULAIN--BONNET Matisse

 Le but de ce script est de g�rer la galerie de l'appareil photo, notamment la navigation � l'int�rieur de celle-ci ainsi que la
possibilit� de s�lectionner n'importe quelle photo et de zoomer dessus. 

/!\ Ce script est directement reli� au script "CameraManager.cs" /!\
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class GalleryManager : MonoBehaviour
{
    public static GalleryManager instance { get; private set; }


    [System.Serializable]
    public class AnimatedImageCameraMenu
    {
        public int index;
        public Image image;
        public Animator animator;
        public string _focus;
        public string _unfocus;


    }


    private int pageIndex = 0;

    private int nbOfPicsPerPage;

    int _indexGallery = 0;



 

    Coroutine loadingCoro;

    [Header("Lists")]
    [SerializeField]
    public List<AnimatedImageCameraMenu> imagesUI;

    [HideInInspector]
    public List<Animator> animators;


    [HideInInspector]
    public bool zoomedOnPhoto = false;

    AnimatedImageCameraMenu curSelectedImage = null;



    private void OnEnable()
    {
        nbOfPicsPerPage = imagesUI.Count;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance pr�c�dente

        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && curSelectedImage != null)
        {
            curSelectedImage.animator.SetTrigger("ZoomIn");
            zoomedOnPhoto = true;

            if (zoomedOnPhoto)
            {
                foreach(AnimatedImageCameraMenu image in imagesUI)
                {
                    if(image != curSelectedImage)
                    {
                        image.image.gameObject.SetActive(false); //desactive les autres images quand on en affiche une en grand
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && curSelectedImage != null && zoomedOnPhoto)
        {
            curSelectedImage.animator.SetTrigger("ZoomOut");
            zoomedOnPhoto = false;

            if (!zoomedOnPhoto)
            {
                foreach (AnimatedImageCameraMenu image in imagesUI)
                {
                    if (image != curSelectedImage)
                    {
                        image.image.gameObject.SetActive(true); //reactive les images quand on dezoome
                    }
                }
            }
        }

    }


    public void OnGalleryUpdatePage()
    {

        int _spritecount = CameraManager.instance._spriteList.Count;

        for (int i = 0; i < ((_spritecount < imagesUI.Count) ? _spritecount : imagesUI.Count); i++)
        {

            if ((i + nbOfPicsPerPage * pageIndex) < CameraManager.instance._spriteList.Count)
            {

                imagesUI[i].image.sprite = CameraManager.instance._spriteList[i + nbOfPicsPerPage * pageIndex];

            }
            else
            {
                imagesUI[i].image.sprite = null;
            }


        }


        if (loadingCoro != null)
            StopCoroutine(loadingCoro);

        foreach(var image in imagesUI)
        {
            image.image.gameObject.SetActive(true);
        }

        loadingCoro = StartCoroutine(DisplayPics());

    }


    private void SelectImage(AnimatedImageCameraMenu _imagetoselect)
    {

        if (curSelectedImage != null)
        {
            curSelectedImage.animator.SetTrigger(curSelectedImage._unfocus);
        }

        curSelectedImage = _imagetoselect;
        curSelectedImage.animator.SetTrigger(curSelectedImage._focus);
        _indexGallery = curSelectedImage.index;
    
    }



    /// <summary>
    /// -1 => previous / 1 => next
    /// </summary>
    /// <param name="_dir"></param>
    public void selectNextOrPrevious(int _dir)
    {
        if (zoomedOnPhoto)
        {
            return;
        }

        _indexGallery += _dir;

        bool _hasChangedPage = false;

        if (_indexGallery < 0)
        {
            _indexGallery = imagesUI.Count + _indexGallery;
            _hasChangedPage = true;

            if (pageIndex > 0)
            {
                pageIndex--;
            }
        }
        else if (_indexGallery >= imagesUI.Count)
        {
            _indexGallery %= imagesUI.Count;
            _hasChangedPage = true;
            pageIndex++;

            //Mettre une s�curit� pour qu'on puisse pas changer de page s'il n'y a pas de photo apr�s

        }

        SelectImage(imagesUI[_indexGallery]);



        if (_hasChangedPage)
            OnGalleryUpdatePage();

    }


    WaitForSeconds loadingDelay = new WaitForSeconds(0.05f);

    IEnumerator DisplayPics()
    {
        foreach (AnimatedImageCameraMenu _image in imagesUI)
        {
            _image.image.color = Color.black;
        }


        for (int i = 0; i < imagesUI.Count; i++)
        {
            yield return loadingDelay;

            imagesUI[i].image.color = Color.white;
        }

        loadingCoro = null;
    }


}