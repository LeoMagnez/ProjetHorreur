using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Animator menuAnimation;
    public bool _menuIsUp = false;
    public bool _isMenuUp = false;


    public Slider _slider;

    public limits limit;

    public bool InvertAxes = false;

    #region SINGLETON
    public static MenuManager instance { get; private set; }
    #endregion

    #region AWAKE
    private void Awake()
    {
        Application.targetFrameRate = (int)limit;

        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente

        instance = this;
    }
    #endregion

    private void Update()
    {
        if (CameraManager.instance.cameraTuto)
        {
            menuController();
        }
    }

    public void menuController()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !CameraManager.instance._isUIup && !CameraManager.instance._isCameraUp)
        {
            if (!_isMenuUp)
            {
                MenuUp(); //lève l'appareil
            }
            else
            {
                MenuDown();//range l'appareil
            }

            
        }
    }

    public void MenuUp()
    {
        menuAnimation.SetTrigger("MenuUiUp");
        _isMenuUp = true;
        _menuIsUp = true;

        CameraManager.instance._isCameraUp = false;
        CameraManager.instance.canPlay = false; //empêche le joueur de bouger lorsqu'il est dans la galerie
    }

    public void MenuDown()
    {
        menuAnimation.SetTrigger("MenuUiDown");
        _isMenuUp = false;
        _menuIsUp = false;


        CameraManager.instance.canPlay = true; //permet au joueur de bouger a nouveau
    }

    public void setInvertAxes()
    {
        InvertAxes = !InvertAxes;   
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetSensivity()
    {
        MouseLook.mouseSensitivity = _slider.value;
    }

    public enum limits
    {
        noLimit = 0,
        limit5 = 5,
        limit30 = 30,
        limit60 = 60,
        limit80 = 80,
        limit120 = 120
    }

    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void LowQ()
    {
        QualitySettings.SetQualityLevel(5, true);
    }

    public void MediumQ()
    {
        QualitySettings.SetQualityLevel(4, true);
    }

    public void highQ()
    {
        QualitySettings.SetQualityLevel(3, true);
    }*/




}
