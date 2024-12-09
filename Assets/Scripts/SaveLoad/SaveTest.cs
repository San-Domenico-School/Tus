using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveTest : MonoBehaviour
{
    [SerializeField] GameObject gameObjectWithTexture;

    String filePath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave/texture.png";
    
    void LateUpdate()
    {
        SaveTexture();
    }
    private void SaveTexture()
    {
        Texture2D image = (Texture2D) gameObjectWithTexture.GetComponent<Renderer>().material.mainTexture;
        
        System.IO.File.WriteAllBytes(filePath, image.EncodeToPNG());
    }
}
