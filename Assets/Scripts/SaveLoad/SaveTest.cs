using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveTest : MonoBehaviour
{
    [SerializeField] GameObject gameObjectWithTexture;
    [SerializeField] bool runScript;
    bool wasRun;

    String filePath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave/texture.png";
    
    void LateUpdate()
    {
        if (wasRun != runScript)
        {
            //SaveTexture();
            Debug.Log("Regular loop start at: " + DateTime.Now.ToString("HH:mm:ss.fff"));
            Debug.Log("Normal: " + ObjectStatisticsUtility.CalculateObjectArea(gameObjectWithTexture));
            Debug.Log("Regular loop done at: " + DateTime.Now.ToString("HH:mm:ss.fff"));
           // Debug.Log("Parallel: " + ObjectStatisticsUtility.CalculateObjectAreaParallel(gameObjectWithTexture));
            //Debug.Log("Parallel loop done at: " + DateTime.Now.ToString("HH:mm:ss.fff"));

            wasRun =  runScript;
        }
    }
    
    private void SaveTexture()
    {
        Texture2D image = (Texture2D) gameObjectWithTexture.GetComponent<Renderer>().sharedMaterial.mainTexture;
        
        System.IO.File.WriteAllBytes(filePath, image.EncodeToPNG());
    }
}
