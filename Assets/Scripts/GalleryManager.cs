/*ETPA 2022 - 2023 // JV2.1 PROJET S4 - GROUPE 4 // GalleryManager.cs
 
 Programmers - ALBOUYS Evangeline, MAGNEZ Léo, POULAIN--BONNET Matisse

 Le but de ce script est de gérer la galerie de l'appareil photo, notamment la navigation à l'intérieur de celle-ci ainsi que la
possibilité de sélectionner n'importe quelle photo et de zoomer dessus. 

/!\ Ce script est directement relié au script "CameraManager.cs" /!\
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    Vector2[] OriginalPos = new Vector2[4];

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

    [HideInInspector]
    AnimatedImageCameraMenu curSelectedImage = null;

    public TextMeshProUGUI debugText;


    private void OnEnable()
    {
        nbOfPicsPerPage = imagesUI.Count;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente

        instance = this;

        foreach (var image in imagesUI)
        {
            OriginalPos[image.index] = image.image.rectTransform.localPosition;
        }
    }

    private void Update()
    {
        //debugText.text = "Img rect : \n" + CameraManager.instance._spriteList[0].rect.ToString() + "\n" + "\n" + "Is img packed ?\n" + CameraManager.instance._spriteList[0].packed + "\n" + "\n" + "Texture reference : \n" + CameraManager.instance._spriteList[0].texture + "\n" + "\n" + "Flags : \n" + CameraManager.instance._spriteList[0].hideFlags;
        //Debug.Log(CameraManager.instance._imgImportanteIndex);
        if (Input.GetKeyDown(KeyCode.E) && curSelectedImage != null)
        {
            //curSelectedImage.animator.SetTrigger("ZoomIn");
            curSelectedImage.image.rectTransform.localPosition = Vector2.zero;
            curSelectedImage.image.rectTransform.localScale = Vector2.one * 2f;
            zoomedOnPhoto = true;

            /*if (CameraManager.instance._objetImportantGallery == true) //Si un objet important a été pris en photo
            {
                Debug.Log("photo importante");
                CameraManager.instance._porte.SetActive(false); //On désactive la porte
            }*/

            if (zoomedOnPhoto)
            {
                foreach(AnimatedImageCameraMenu image in imagesUI)
                {
                    if(image != curSelectedImage)
                    {
                        image.image.gameObject.SetActive(false); //desactive les autres images quand on en affiche une en grand
                    }
                }
                if (curSelectedImage.index == CameraManager.instance._imgImportanteIndex)
                {
                    CameraManager.instance._porte.SetActive(false); //On désactive la porte
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && curSelectedImage != null && zoomedOnPhoto)
        {
            curSelectedImage.animator.SetTrigger("ZoomOut");
            zoomedOnPhoto = false;
            curSelectedImage.image.rectTransform.localPosition = OriginalPos[curSelectedImage.index];
            curSelectedImage.image.rectTransform.localScale = Vector2.one;
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

        if (!CameraManager.instance._isUIup && curSelectedImage != null)
        {
            curSelectedImage.animator.SetTrigger("ZoomOut");
            zoomedOnPhoto = false;
            curSelectedImage.image.rectTransform.localPosition = OriginalPos[curSelectedImage.index];
            curSelectedImage.image.rectTransform.localScale = Vector2.one;
            StartCoroutine(CameraManager.instance.WaitBeforeDisablingCamera());
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

                if(CameraManager.instance._imgImportanteIndex == i)
                {

                    //LOGIQUE CHANGEMENT VISUEL DE PHOTO IMPORTANTE
                }

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
            //curSelectedImage.animator.SetTrigger(curSelectedImage._unfocus);

            RectTransform rectTransformOld = curSelectedImage.image.rectTransform;
            rectTransformOld.localScale = Vector3.one;
            //rectTransformOld.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            /*rectTransformNew.anchorMax = new Vector2(0.75f, 0.75f);
            rectTransformOld.anchorMin = new Vector2(0, 0);*/
        }

        curSelectedImage = _imagetoselect;
        //curSelectedImage.animator.SetTrigger(curSelectedImage._focus);
        _indexGallery = curSelectedImage.index;

        RectTransform rectTransformNew = curSelectedImage.image.rectTransform;
        rectTransformNew.localScale = Vector3.one * 1.1f;
        Debug.Log(rectTransformNew.localScale);
        //rectTransformNew.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10f);
        /*rectTransformNew.anchorMax = new Vector2(0.75f, 0.75f);
        rectTransformNew.anchorMin = new Vector2(0.25f, 0.25f);*/

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

            //Mettre une sécurité pour qu'on puisse pas changer de page s'il n'y a pas de photo après

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
        SelectImage(imagesUI[_indexGallery]);
    }


}