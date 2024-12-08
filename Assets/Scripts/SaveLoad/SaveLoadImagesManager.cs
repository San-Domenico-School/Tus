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
    //String saveImagesPath = Application.persistentDataPath; // use when builing for Quest
    String saveImagesPath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave"; // use when tesing with pc on Seamus computer 
    
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
            File.WriteAllBytes(Path.Combine(saveImagesPath, gameObject.name), image.EncodeToPNG());
        }
    }

    private void LoadImages()
    {
        if (prepairWorld == null || prepairWorld.GetComponent<PrepairWorld>() == null)
            return; 

        foreach (GameObject gameObject in prepairWorld.GetComponent<PrepairWorld>().paintableObjects)
        {
            if (gameObject.GetComponent<Renderer>().material.mainTexture == null)
            {
                gameObject.GetComponent<Renderer>().material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, prepairWorld.GetComponent<PrepairWorld>().texelDensity);
            }
            else 
            {
                
            }
        }
    }
}
