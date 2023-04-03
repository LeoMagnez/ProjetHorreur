
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;

public class SaveRenderTextureToFile
{
    

    public static Texture2D tex;

    
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


        //string path = Directory.GetCurrentDirectory() + "/Screenshots/";

        /*if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }*/

        File.WriteAllBytes(path, bytes);
        //AssetDatabase.ImportAsset(path);


        Debug.Log("Saved to " + path);
    }

    
    /*public static bool SaveRTToFileValidation()
    {
        return Selection.activeObject is RenderTexture;
    }*/
}