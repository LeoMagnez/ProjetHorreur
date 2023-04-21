using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TPVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.falling = true;
            other.gameObject.transform.position = new Vector3(10f, 2f, 3f);
            StartCoroutine(FallingCoroutine());
        }
        
    }

    IEnumerator FallingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        PlayerMovement.falling = false; 
    }

}
