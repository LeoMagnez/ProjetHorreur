using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject ghost;

    [SerializeField] private AK.Wwise.Event objPlaceFeedback;
    [SerializeField] private GameObject sfxObj;

    public void MoveObject(Vector3 _targetPos)
    {
        transform.position = _targetPos;
        MeshRenderer mesh;

        if (gameObject.TryGetComponent<MeshRenderer>(out mesh))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;

        }
        else
        {
            List<MeshRenderer> tempMR = new List<MeshRenderer>();
            tempMR = gameObject.GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>();

            foreach (MeshRenderer mr in tempMR)
            {
                mr.enabled = true;
            }
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void MoveObjectToAnchor(Vector3 _anchor)
    {
        // Get the anchor's position and rotation in world space
        Vector3 anchorWorldPos = _anchor;
        //Quaternion anchorWorldRot = _anchor.transform.rotation;

        // Calculate the target position in world space by adding the anchor's position and the local offset
        //Vector3 targetWorldPos = anchorWorldPos + anchorWorldRot * anchor.vectorAnchorToOrigin;

        // Set the position and rotation of the GameObject to the target world position and rotation
        transform.position = anchorWorldPos;
        //transform.rotation = anchorWorldRot;

        // Enable the MeshRenderer and Collider of the GameObject

        MeshRenderer mesh;

        if (gameObject.TryGetComponent<MeshRenderer>(out mesh))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            if(ghost != null)
            {
                ghost.SetActive(false);
            }
        }

        List<MeshRenderer> tempMR = new List<MeshRenderer>();
        tempMR = gameObject.GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>();
        
        foreach (MeshRenderer mr in tempMR)
        {
            mr.enabled = true;
        }

        List<BoxCollider> tempCol = new List<BoxCollider>();
        tempCol = gameObject.GetComponentsInChildren<BoxCollider>().ToList<BoxCollider>();
        
        foreach (BoxCollider col in tempCol)
        {
            col.enabled = false;
        }

        List<MeshCollider> tempMC = new List<MeshCollider>();
        tempMC = gameObject.GetComponentsInChildren<MeshCollider>().ToList<MeshCollider>();

        foreach (MeshCollider mc in tempMC)
        {
            mc.enabled = true;
        }

        gameObject.GetComponent<Collider>().enabled = true;
        objPlaceFeedback.Post(sfxObj);
    }

    public void HideObject()
    {
        MeshRenderer mesh;

        if (gameObject.TryGetComponent<MeshRenderer>(out mesh))
        {

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            
        }
        
        
        List<MeshRenderer> tempMR = new List<MeshRenderer> ();
        tempMR = gameObject.GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>();

        foreach(MeshRenderer mr in tempMR)
        {
                mr.enabled = false;
        }

        List<BoxCollider> tempCol = new List<BoxCollider>();
        tempCol = gameObject.GetComponentsInChildren<BoxCollider>().ToList<BoxCollider>();

        foreach (BoxCollider col in tempCol)
        {
            col.enabled = false;
        }

        List<MeshCollider> tempMC = new List<MeshCollider>();
        tempMC = gameObject.GetComponentsInChildren<MeshCollider>().ToList<MeshCollider>();

        foreach (MeshCollider mc in tempMC)
        {
            mc.enabled = false;
        }



        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;



    }
}
