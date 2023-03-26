using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour

{
    private int _screenNumber = 0;

    private bool _isCameraUp = false;

    public bool _takingPhoto = false;

    public GameObject UI;

    public MeshRenderer MeshRenderer;
    public Material _whiteMaterial;
    public Material _blackMaterial;
    public Texture image;

    public List<Image> _takenPictures = new List<Image>();
    public Texture2D test;

    public RenderTexture rt;

    private void Start()
    {

        if(test != null)
        {
            Debug.Log("J'ai un truc");
        }
        else
        {
            Debug.Log("J'ai rien trouv�");
        }

        test = Resources.Load<Texture2D>("capture0");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1)) 
        {
            if (!_isCameraUp)
            {
                cameraUp();
            }
            else 
            {
                cameraDown();
            }
        }
        //cameraUp();
        changeMaterial();
        takePhoto();
    }

    private void FixedUpdate()
    {
        
    }

    private void cameraUp()
    {
        _isCameraUp = true;
        UI.SetActive(true);
    }
    private void cameraDown()
    {
        _isCameraUp = false;
        UI.SetActive(false);
    }

    private void takePhoto()
    {
        //Possibilit� de mettre une condition pour limiter le nombre de prise de photo avec _screenNumber
        if (Input.GetMouseButtonDown(0) && _isCameraUp)
        {
            _takingPhoto = true;
            Raycast();
            Debug.Log("oui je marche");
            //ScreenCapture.CaptureScreenshot("Assets\\Screenshot\\capture" + _screenNumber++ + ".png");
            //AssetDatabase.Refresh();
            SaveRenderTextureToFile.SaveRTToFile(rt, _screenNumber);
            _screenNumber++;
            Image temp = Resources.Load("Assets\\Screenshot\\capture" + _screenNumber + ".png") as Image;
            _takenPictures.Add(temp);
            //"Assets\\Screenshot\\capture" + _screenNumber + ".png"
        }

    }
    private void changeMaterial()
    {
        //MeshRenderer.material.SetTexture("_BaseMap", image);
    }

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

                MeshRenderer.GetComponent<MeshRenderer>().material = _blackMaterial;
                _takingPhoto = false;
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
                MeshRenderer.GetComponent<MeshRenderer>().material = _whiteMaterial;
                _takingPhoto = false;
            }
        }
    }


}
