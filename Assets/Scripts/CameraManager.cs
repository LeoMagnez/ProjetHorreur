using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour

{
    private int _screenNumber = 0;


    public MeshRenderer MeshRenderer;
    public Material material;
    public Texture image;

    private void Update()
    {
        takePhoto();
        changeMaterial();
    }

    private void takePhoto()
    {
        //Possibilité de mettre une condition pour limiter le nombre de prise de photo avec _screenNumber
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("oui je marche");
            ScreenCapture.CaptureScreenshot("D:\\Unity Project\\ProjetHorreur\\Assets\\Screenshot\\capture" + _screenNumber++ + ".png");
            AssetDatabase.Refresh();
        }
    }
    private void changeMaterial()
    {
        MeshRenderer.material.SetTexture("_BaseMap", image);
    }


}
