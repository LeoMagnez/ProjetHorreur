using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TPVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = new Vector3(10f, 2f, 3f);
    }
}
