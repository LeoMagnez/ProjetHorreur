
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

public class SaveRenderTextureToFile
{
    

    public static Texture2D tex;
    public Camera photoCamera;
    
    public static Sprite SaveRTToFile(RenderTexture rt, int _screenNumber)
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
        Debug.Log("Saved to " + path);
        //AssetDatabase.ImportAsset(path);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));


    }
    public static Sprite ToTexture2D(RenderTexture rTex, int _screenNumber)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rTex;
        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rTex.width, rTex.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();
        RenderTexture.active = currentActiveRT;

        //fonction ecriture fichier

        //WritePhotoToFile(rTex, _screenNumber);

        string path = Directory.GetCurrentDirectory() + "/Screenshots/";

        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

    public static void WritePhotoToFile(Sprite[] photos)
    {
        int i = 0;
        foreach (Sprite item in photos)
        {
            Texture2D imagetosave = item.texture;
            byte[] bytes;
            bytes = imagetosave.EncodeToPNG();

            string path = "Screenshots\\capture" + i + ".png";
            File.WriteAllBytes(path, bytes);
            i++;

        }
        /*RenderTexture.active = rt;
        Graphics.Blit(rt, rt);
        tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        byte[] bytes;
        bytes = tex.EncodeToPNG();*/

        //tring path = "Screenshots\\capture" + _screenNumber + ".png";


    }

    /*public static bool SaveRTToFileValidation()
    {
        return Selection.activeObject is RenderTexture;
    }*/
}