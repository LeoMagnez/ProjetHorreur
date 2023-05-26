using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Trigger_poubelle : MonoBehaviour
{

    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("qqc touche");
        if (other.gameObject.CompareTag("_photoNormale"))
        {
            Debug.Log("Désactivation porte");
            door.SetActive(false);
        }
    }
}
