using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(10, 2, 3);

    }

}
