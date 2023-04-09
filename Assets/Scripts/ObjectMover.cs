using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{



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
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
<<<<<<< Updated upstream
=======
        
        
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
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
>>>>>>> Stashed changes
    }

    public void HideObject()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
