using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_shenanigans : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event fireSfx;
    [SerializeField] private GameObject chaudiere;

    void Update()
    {
        if (!gameObject.GetComponent<MeshRenderer>().enabled)
        {
            fireSfx.Stop(chaudiere);
        }
    }
}
