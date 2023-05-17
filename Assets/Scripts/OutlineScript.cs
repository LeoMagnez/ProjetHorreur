using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;

    private Renderer outlineRenderer;

    private void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlinedObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlinedObject.transform.localScale = transform.localScale * scaleFactor;

        outlinedObject.layer = 3;
        outlinedObject.tag = "Untagged";

        Renderer rend = outlinedObject.GetComponent<Renderer>();
        rend.material = outlineMat;

        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlinedObject.GetComponent<OutlineScript>().enabled = false;
        outlinedObject.GetComponent<MeshCollider>().enabled = false;
        outlinedObject.GetComponent<BoxCollider>().enabled = false;
        rend.enabled = true;

        return rend;
    }
}
