using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume_menu : MonoBehaviour
{
    public Slider thisSlider;
    public float masterVolume;

    public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = thisSlider.value;

        if(whatValue == "Master")
        {
            masterVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
        }
    }
}
