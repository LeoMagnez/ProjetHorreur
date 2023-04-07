using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class FurnitureAnchor : MonoBehaviour
{


    [SerializeField]
    public Vector3 anchorLocalPos;

    [HideInInspector]
    [Tooltip("pas touche")]
    public Vector3 worldPos = Vector3.zero;

    [HideInInspector]
    [Tooltip("pas touche")]
    public Vector3 vectorAnchorToOrigin = Vector3.zero;

    [SerializeField]
    [Range(0.1f, 0.5f)]
    float radius = 0.3f;

    [SerializeField]
    Color color = Color.magenta;

    [SerializeField]
    bool LOCK_ANCHOR = false;

    void OnEnable()
    {
        FurnitureAnchorsManager.allTheAnchors.Add(this);
 
    }

    private void OnDisable()
    {
        FurnitureAnchorsManager.allTheAnchors.Remove(this);
    }

    private void OnValidate()
    {
        if (LOCK_ANCHOR)
            return;

        Vector3 _gizmoPos = GetComponent<MeshRenderer>().bounds.center;

        //

        Vector3 _boundSize = GetComponent<MeshRenderer>().localBounds.size;

        Debug.Log(_boundSize);

        // Il faut rotater la partie  locale

        //Debug.Log(_boundSize);

        Vector3 _rotatedLocalPos = Vector3.zero;

        _rotatedLocalPos.x = anchorLocalPos.x * _boundSize.x * transform.localScale.x;
        _rotatedLocalPos.y = anchorLocalPos.y * _boundSize.y * transform.localScale.y;
        _rotatedLocalPos.z = anchorLocalPos.z * _boundSize.z * transform.localScale.z;

        _rotatedLocalPos = transform.rotation * _rotatedLocalPos;

        worldPos = _gizmoPos + _rotatedLocalPos;


        // On bake le vecteur de l'ancre vers l'origine (pour replacer bien le truc plus tard!) ameno

        vectorAnchorToOrigin = transform.position - worldPos;


    }

    public Vector3 GetWorldPos()
    {

        return worldPos;
    }

    void OnDrawGizmos()
    {/*
        if (transform.hasChanged)
            OnValidate();
        */
        //anchorLocalPos = GetComponent<Renderer>().bounds.center;
        Gizmos.color = color;
        Gizmos.DrawSphere(worldPos, radius);


    }
}
