using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{

    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void HideObject()
    {
        MeshRenderer mesh;

        if (gameObject.TryGetComponent<MeshRenderer>(out mesh))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            
        }
        else
        {
            List<MeshRenderer> tempMR = new List<MeshRenderer> ();
            tempMR = gameObject.GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>();
            Debug.Log(tempMR.Count);
            foreach(MeshRenderer mr in tempMR)
            {
                mr.enabled = false;
            }
        }


        gameObject.GetComponent<Collider>().enabled = false;



    }
}
