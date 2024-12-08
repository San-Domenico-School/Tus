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


    private void OnEnable() 
    {
        LoadImages();
    }

    private void OnApplicationQuit() 
    {
        SaveImages();
    }

    // Load all the images onto the game objects 
    private void LoadImages()
    {
        float texelDensity = prepairWorld.GetComponent<PrepairWorld>().texelDensity;

        if (prepairWorld == null || prepairWorld.GetComponent<PrepairWorld>() == null)
            return; 

        foreach (GameObject gameObject in prepairWorld.GetComponent<PrepairWorld>().paintableObjects)
        {
            if (File.Exists(Path.Combine(saveImagesPath, gameObject.name)))
            {
                byte[] imageData = File.ReadAllBytes(Path.Combine(saveImagesPath, gameObject.name));
                Texture2D objectTexture = new Texture2D(2,2);
                ImageConversion.LoadImage(objectTexture, imageData);
                gameObject.GetComponent<Renderer>().material.mainTexture = objectTexture;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
            }
        }
    }

    // Saves all the images on game object that can be painted (in array of object gotton from PrepairWorld)
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

}
