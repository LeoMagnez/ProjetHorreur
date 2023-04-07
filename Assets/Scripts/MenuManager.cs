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
        if (Input.GetKeyUp(KeyCode.Escape))
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
    }

    public void MenuDown()
    {
        menuAnimation.SetTrigger("MenuUiDown");
        _isMenuUp = false;
        _menuIsUp = false;
    }
}
