using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.Unity.VisualStudio.Editor;

/**************************************************
 * Attached to: SaveManager
 * Purpose: saves and loads the texture you paint on 
 * Author: Seamus
 * Version: 1.0
 *************************************************/

public class SaveLoadImagesManager : MonoBehaviour
{
    //String filePath = Application.persistentDataPath; // use when builing for Quest
    String filePath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave"; // use when tesing with pc on Seamus computer 
    
    [SerializeField] GameObject prepairWorld;

    private void OnApplicationQuit() 
    {
        SaveImages();
    }

    //saves all the images on game object that can be painted on (list of object gotton from PrepairWorld)
    private void SaveImages()
    {
        if (prepairWorld == null || prepairWorld.GetComponent<PrepairWorld>() == null)
            return; 

        foreach (GameObject gameObject in prepairWorld.GetComponent<PrepairWorld>().paintableObjects)
        {
            if (gameObject.GetComponent<Renderer>().material.mainTexture == null)
                return;
            
            Texture2D image = (Texture2D) gameObject.GetComponent<Renderer>().material.mainTexture;
            System.IO.File.WriteAllBytes(Path.Combine(filePath, gameObject.name), image.EncodeToPNG());
        }
    }

    
}
