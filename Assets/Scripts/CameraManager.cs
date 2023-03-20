using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour

{
    private int _screenNumber = 0;

    private void Update()
    {
        //Possibilité de mettre une condition pour limiter le nombre de prise de photo avec _screenNumber
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("oui je marche");
            ScreenCapture.CaptureScreenshot("C://Users//game2//Documents//GitHub//ProjetHorreur//Assets//Screenshot//capture"+ _screenNumber++ + ".png");
        }
    }


}
