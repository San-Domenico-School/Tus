using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScenePictureScript : MonoBehaviour
{
    [SerializeField] string PictureName = "default";
    [SerializeField] Texture2D defaulttex;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        Texture2D texture;

        path = Path.Combine(Application.persistentDataPath, (PictureName + ".png"));

        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
        }
        else {
            texture = defaulttex;
        }

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;

        float aspectRatio = (float)texture.width / texture.height;
        Vector3 oldscale = transform.localScale;
        Vector3 newscale = new Vector3(aspectRatio, 1, 1);
        transform.localScale = new Vector3(oldscale.x * newscale.x, oldscale.y * newscale.y, oldscale.z * newscale.z);
    }
}
