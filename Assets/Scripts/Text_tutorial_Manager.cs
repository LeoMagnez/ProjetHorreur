using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Text_tutorial_Manager : MonoBehaviour
{
    public TMP_Text text;
    public string TextTuto;



    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    public void SetText()
    {
        text.SetText("" + TextTuto);
    }
}
