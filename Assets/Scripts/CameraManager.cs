using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour

{
    private int _screenNumber = 0;

    private bool _isCameraUp = false;

    public GameObject UI;

    public MeshRenderer MeshRenderer;
    public Material material;
    public Texture image;

    private void Update()
    {
        cameraUp();
        changeMaterial();
        takePhoto();
    }

    private void cameraUp()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isCameraUp= true;
            UI.SetActive(true);
        }
    }

    private void takePhoto()
    {
        //Possibilit� de mettre une condition pour limiter le nombre de prise de photo avec _screenNumber
        if (Input.GetMouseButtonDown(0) && _isCameraUp)
        {
            Debug.Log("oui je marche");
            ScreenCapture.CaptureScreenshot("Assets\\Screenshot\\capture" + _screenNumber++ + ".png");
            AssetDatabase.Refresh();
        }

    }
    private void changeMaterial()
    {
        MeshRenderer.material.SetTexture("_BaseMap", image);
    }


}
