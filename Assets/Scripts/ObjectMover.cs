using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(FurnitureAnchor))]

public class ObjectMover : MonoBehaviour
{
    FurnitureAnchor anchor;

    private void OnEnable()
    {
        anchor = GetComponent<FurnitureAnchor>();
    }

    public void MoveObject(Vector3 _targetPos)
    {



        transform.position = _targetPos;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        
        
    }

    public void MoveObjectToAnchor(FurnitureAnchor _anchor)
    {
        transform.position = _anchor.GetWorldPos();
        transform.position += anchor.vectorAnchorToOrigin;

        transform.rotation = _anchor.transform.rotation;


        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void HideObject()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }


}
