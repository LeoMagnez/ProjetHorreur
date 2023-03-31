using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class GalleryManager : MonoBehaviour
{

    private int pageIndex = 0;

    private int nbOfPicsPerPage;



    int _indexGallery = 0;

    [System.Serializable]
    public class AnimatedImageCameraMenu
    {
        public int index;
        public Image image;
        public Animator animator;
    }




    [SerializeField]
    public List<AnimatedImageCameraMenu> imagesUI;


    Coroutine loadingCoro;

    [HideInInspector]
    public List<Animator> animators;

    AnimatedImageCameraMenu curSelectedImage = null;


    private void OnEnable()
    {
        nbOfPicsPerPage = imagesUI.Count;
    }



    public void OnGalleryUpdatePage()
    {

        int _spritecount = CameraManager.instance._spriteList.Count;

        for (int i = 0; i < ((_spritecount < imagesUI.Count)? _spritecount : imagesUI.Count); i++)
        {

            if((i + nbOfPicsPerPage * pageIndex) < CameraManager.instance._spriteList.Count)
            {

                imagesUI[i].image.sprite = CameraManager.instance._spriteList[i + nbOfPicsPerPage * pageIndex];

            }
            else
            {
                imagesUI[i].image.sprite = null;
            }

            
        }

       /* if (Input.GetKeyDown(KeyCode.K))
        {

            _animator.SetBool("Focus", true);
        }*/

        if (loadingCoro != null)
            StopCoroutine(loadingCoro);

        loadingCoro = StartCoroutine(DisplayPics());

    }


    private void SelectImage(AnimatedImageCameraMenu _imagetoselect)
    {
        if (curSelectedImage != null)
            curSelectedImage.animator.SetTrigger("Unfocus");


        curSelectedImage = _imagetoselect;

        curSelectedImage.animator.SetTrigger("Focus");

        _indexGallery = curSelectedImage.index;

    }


    /// <summary>
    /// -1 => previous / 1 => next
    /// </summary>
    /// <param name="_dir"></param>
    public void selectNextOrPrevious(int _dir)
    {
        _indexGallery += _dir;

        bool _hasChangedPage = false;

        if(_indexGallery < 0)
        {
            _indexGallery = imagesUI.Count + _indexGallery;
            _hasChangedPage = true;

            if(pageIndex > 0)
            {
                pageIndex--;
            }
        }
        else if(_indexGallery >= imagesUI.Count)
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


    WaitForSeconds loadingDelay = new WaitForSeconds(0.3f);

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
