using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*[ExecuteAlways]*/
public class FurnitureAnchor : MonoBehaviour
{
    [SerializeField]
    int anchorNumberX;

    [SerializeField]
    int anchorNumberY;

    [SerializeField]
    float offsetMultiplier;

    [Header("Position des ancres")]
    [SerializeField]
    public Vector3 anchorLocalPos;

    [SerializeField]
    Vector3 anchorOffset = Vector3.zero;

    [HideInInspector]
    [Tooltip("pas touche")]
    public Vector3 worldPos = Vector3.zero;

    [HideInInspector]
    [Tooltip("pas touche")]
    public Vector3 vectorAnchorToOrigin = Vector3.zero;

    [Header("Radius")]
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float radius = 0.3f;

    [Header("Rotation des ancres")]
    [SerializeField]
    public float desiredRotationAngle = 45.0f;

    public float rotationX;
    public float rotationY;
    public float rotationZ;

    [SerializeField]
    [Header("Distinction murs/meubles (PAS TOUCHE)")]
    Color color = Color.magenta;

    [SerializeField]
    [Header ("Toujours activer après une modification (désac avant)")]
    bool LOCK_ANCHOR = false;

    [SerializeField]
    public InstableRoom instableRoom;

    [HideInInspector]
    public bool isActive = true;

    

    public List<Vector3> anchorList = new List<Vector3>();

    void OnEnable()
    {
        //FurnitureAnchorsManager.allTheAnchors.Add(this);
 
    }

    private void OnDisable()
    {
        //FurnitureAnchorsManager.allTheAnchors.Remove(this);
    }

    private void Start()
    {
        anchorList.Clear();
        for (int i = 1; i < anchorNumberX-1; i++)
        {
            for (int j = 1; j < anchorNumberY-1; j++)
            {
                // Calculate the local position of the Gizmo
                Vector3 localPosition = new Vector3(anchorOffset.x * i, anchorOffset.y * j, 0);

                // Apply rotation in local space
                Vector3 rotatedLocalPosition = transform.TransformDirection(localPosition);

                // Apply desired rotation angles
                Quaternion rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
                rotatedLocalPosition = rotation * rotatedLocalPosition;

                // Calculate the world position of the Gizmo
                Vector3 worldPosition = worldPos + rotatedLocalPosition;

                anchorList.Add(worldPosition);
            }
        }
    }


    private void OnValidate()
    {
        if (LOCK_ANCHOR)
            return;

        Vector3 _gizmoPos = Vector3.zero;
        Vector3 _boundSize = Vector3.one;

        MeshRenderer mesh;
        if(TryGetComponent<MeshRenderer>(out mesh))
        {
            _gizmoPos = mesh.bounds.center;
            _boundSize = mesh.localBounds.size;
        }
       



        // Get the rotated local position
        Vector3 _rotatedLocalPos = Vector3.Scale(anchorLocalPos, _boundSize);

        // Transform the local position to world space, taking into account the rotation of the object
        _rotatedLocalPos = transform.TransformVector(_rotatedLocalPos);

        worldPos = _gizmoPos + _rotatedLocalPos;

        // Calculate the vector from the anchor to the object's position
        vectorAnchorToOrigin = transform.position - worldPos;

        //anchorNumber = Mathf.Max(1, anchorNumber);
    }

    public Vector3 GetWorldPos()
    {

        return worldPos;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        // Draw the Gizmo
        Gizmos.DrawSphere(worldPos, radius);

        /*for (int i = 1; i < anchorNumberX - 1; i++)
        {
            for (int j = 1; j < anchorNumberY - 1; j++)
            {
                // Calculate the local position of the Gizmo
                Vector3 localPosition = new Vector3(anchorOffset.x * i, anchorOffset.y * j, 0);

                // Apply rotation in local space
                Vector3 rotatedLocalPosition = transform.TransformDirection(localPosition);

                // Apply desired rotation angles
                Quaternion rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
                rotatedLocalPosition = rotation * rotatedLocalPosition;

                // Calculate the world position of the Gizmo
                Vector3 worldPosition = worldPos + rotatedLocalPosition;




            }
        }*/
    }


}

