
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class SaveRenderTextureToFile
{
    

    public static Texture2D tex;

    [MenuItem("Assets/Save RenderTexture to file")]
    public static void SaveRTToFile(RenderTexture rt, int _screenNumber)
    {
        //RenderTexture rt = Selection.activeObject as RenderTexture;

        RenderTexture.active = rt;
        Graphics.Blit(rt, rt);
        tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, rt.width , rt.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        byte[] bytes;
        bytes = tex.EncodeToPNG();

        string path = "Assets\\Resources\\capture" + _screenNumber + ".png";
        System.IO.File.WriteAllBytes(path, bytes);
        //AssetDatabase.ImportAsset(path);
        Debug.Log("Saved to " + path);
    }

    [MenuItem("Assets/Save RenderTexture to file", true)]
    public static bool SaveRTToFileValidation()
    {
        return Selection.activeObject is RenderTexture;
    }
}