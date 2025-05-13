using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * 
 */
public class SceneCameraController : MonoBehaviour
{
    [SerializeField] string PictureName = "default";
    string savepath;

    void Start() {
        string savepath = Application.persistentDataPath;
    }

    public void save()
    {
        Camera rendercamera = gameObject.GetComponent<Camera>();
        RenderTexture renderTexture = new(rendercamera.pixelWidth, rendercamera.pixelHeight, 32);
        rendercamera.targetTexture = renderTexture;
        rendercamera.Render();
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        rendercamera.targetTexture = null;
        RenderTexture.active = active;
        File.WriteAllBytes(Path.Combine(savepath, PictureName), tex.EncodeToPNG());
    }
}
