using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityDropdown : MonoBehaviour
{


    public void DropdownQuality(int index)
    {
        switch (index)
        {
            case 0: QualitySettings.SetQualityLevel(5); Debug.Log("1"); break;
            case 1: QualitySettings.SetQualityLevel(4); Debug.Log("2"); break;
            case 2: QualitySettings.SetQualityLevel(0); Debug.Log("3"); break;
        }
    }

}

