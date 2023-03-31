using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{

    private int pageIndex = 0;

    private int nbOfPicsPerPage;


    [SerializeField]
    List<Image> imagesUI;


    Coroutine loadingCoro;




    private void OnEnable()
    {
        nbOfPicsPerPage = imagesUI.Count;
    }



    public void OnGalleryUpdatePage()
    {

        int _spritecount = CameraManager.instance._spriteList.Count;

        for (int i = 0; i < ((_spritecount < imagesUI.Count)? _spritecount : imagesUI.Count); i++)
        {
            imagesUI[i].sprite = CameraManager.instance._spriteList[i + nbOfPicsPerPage* pageIndex];
        }


        if (loadingCoro != null)
            StopCoroutine(loadingCoro);

        loadingCoro = StartCoroutine(DisplayPics());

    }


    WaitForSeconds loadingDelay = new WaitForSeconds(0.3f);

    IEnumerator DisplayPics()
    {
        foreach (Image _image in imagesUI)
        {
            _image.color = Color.black;
        }


        for (int i = 0; i < imagesUI.Count; i++)
        {
            yield return loadingDelay;

            imagesUI[i].color = Color.white;
        }

        loadingCoro = null;
    }

}
