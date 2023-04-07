using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Animator menuAnimation;
    public bool _menuIsUp = false;
    public bool _isMenuUp = false;

    #region SINGLETON
    public static MenuManager instance { get; private set; }
    #endregion

    #region AWAKE
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente

        instance = this;
    }
    #endregion

    private void Update()
    {
        menuController();
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
}
