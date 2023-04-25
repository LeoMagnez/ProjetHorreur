using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    public void ResDropdown(int index)
    {
        switch (index)
        {
            case 0: Screen.SetResolution(1920, 1080, true); Screen.fullScreen = !Screen.fullScreen; Debug.Log("2"); break;
            case 1: Screen.SetResolution(1366, 768, true); Screen.fullScreen = !Screen.fullScreen; Debug.Log("1"); break;
            case 2: Screen.SetResolution(1280, 720, true); Screen.fullScreen = !Screen.fullScreen; Debug.Log("1"); break;
            case 3: Screen.SetResolution(1024, 768, true); Screen.fullScreen = !Screen.fullScreen; Debug.Log("1"); break;
            case 4: Screen.SetResolution(800, 600, true); Screen.fullScreen = !Screen.fullScreen; Debug.Log("1"); break;
            case 5: Screen.SetResolution(640, 360, true); Screen.fullScreen = !Screen.fullScreen; Debug.Log("1"); break;

        }
    }
}
