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
            other.gameObject.transform.position = new Vector3(10f, 10f, 3f);
            Debug.Log(PlayerMovement.falling);
            StartCoroutine(FallingCoroutine());
        }
        
    }

    IEnumerator FallingCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerMovement.falling = false; 
    }

}
