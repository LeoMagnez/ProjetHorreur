using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstableRoom : MonoBehaviour
{
    [SerializeField] int instableCounter;
    

    public void DecreaseCounter()
    {
        instableCounter--;

        if(instableCounter == 0)
        {
            //Event porte
        }
    }
   
}
