using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Vector3 outlineScaleFactor;
    [SerializeField] private Color outlineColor;

    private Renderer outlineRenderer;

    private void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }

    /*private void Update()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }*/

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
        rend.enabled = true;

        return rend;
    }
}
