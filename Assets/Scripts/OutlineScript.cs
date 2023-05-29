using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    public static OutlineScript instance;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Vector3 outlineScaleFactor;
    [SerializeField] private Color outlineColor;

    private Renderer outlineRenderer;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }

    private void Update()
    {
        if (!gameObject.GetComponent<Renderer>().enabled)
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
                
        }
    }

    Renderer CreateOutline(Material outlineMat, Vector3 scaleFactor, Color color)
    {
        GameObject outlinedObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlinedObject.transform.localScale = new Vector3(outlineScaleFactor.x, outlineScaleFactor.y, outlineScaleFactor.z);

        outlinedObject.layer = 3;
        outlinedObject.tag = "Untagged";

        Renderer rend = outlinedObject.GetComponent<Renderer>();
        rend.material = outlineMat;

        rend.material.SetColor("_OutlineColor", color);
        //rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlinedObject.GetComponent<OutlineScript>().enabled = false;
        outlinedObject.GetComponent<MeshCollider>().enabled = false;
        outlinedObject.GetComponent<BoxCollider>().enabled = false;

        if(outlinedObject.GetComponent<Rigidbody>() != null)
        {
            outlinedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        
        rend.enabled = true;

        return rend;
    }
}
