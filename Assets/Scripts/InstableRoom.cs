using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstableRoom : MonoBehaviour
{
    [SerializeField] int instableCounter;

    public GameObject trigger_scene;
    

    public void DecreaseCounter()
    {
        instableCounter--;

        if(instableCounter == 0)
        {
            trigger_scene.SetActive(true);
        }
    }
   
}
