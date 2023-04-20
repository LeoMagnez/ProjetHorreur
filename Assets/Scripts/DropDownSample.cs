using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownSample : MonoBehaviour
{
    public void DropdownSample(int index)
    {
        switch (index)
        {

            case 0: Application.targetFrameRate = 0; break;
            case 1: Application.targetFrameRate = 80; break;
            case 2: Application.targetFrameRate = 60; break;
            case 3: Application.targetFrameRate = 30; break;
        }
    }
}
