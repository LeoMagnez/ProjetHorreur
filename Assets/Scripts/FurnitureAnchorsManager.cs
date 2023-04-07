using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FurnitureAnchorsManager : MonoBehaviour
{

    public static List<FurnitureAnchor> allTheAnchors = new List<FurnitureAnchor>();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach(FurnitureAnchor anchor in allTheAnchors)
        {
            Vector3 managerPos = transform.position;
            Vector3 furnitureAnchorPos = anchor.worldPos;
            float halfHeight = (managerPos.y - furnitureAnchorPos.y) * 0.5f;
            Vector3 tangentOffset = Vector3.up * halfHeight;

            Handles.DrawBezier(managerPos, furnitureAnchorPos, managerPos - tangentOffset, furnitureAnchorPos + tangentOffset, Color.white, EditorGUIUtility.whiteTexture, 1f);
        }
    }

#endif

}
