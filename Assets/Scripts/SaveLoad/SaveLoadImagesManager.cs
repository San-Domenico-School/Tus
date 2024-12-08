using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/**************************************************
 * Attached to: SaveManager
 * Purpose: saves and loads the texture you paint on 
 * Author: Seamus
 * Version: 1.0
 *************************************************/

public class SaveLoadImagesManager : MonoBehaviour
{
    String saveImagesPath;     

    private void Start() 
    {
        saveImagesPath = Application.persistentDataPath;// use when builing for Quest
        //saveImagesPath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave"; // use when tesing with pc on Seamus computer 

        LoadImages();
        Debug.Log(saveImagesPath);
    }

    private void OnApplicationQuit() 
    {
        SaveImages();
    }

    // Load all the images onto the game objects 
    private void LoadImages()
    {

        float texelDensity = PrepairWorld.texelDensity;

        foreach (GameObject gameObject in PrepairWorld.paintableObjects)
        {

            if (File.Exists(Path.Combine(saveImagesPath, gameObject.name + ".png")))
            {
                byte[] imageData = File.ReadAllBytes(Path.Combine(saveImagesPath, gameObject.name + ".png"));
                Texture2D objectTexture = new Texture2D(2,2);
                ImageConversion.LoadImage(objectTexture, imageData);
                gameObject.GetComponent<Renderer>().material.mainTexture = objectTexture;
                Debug.Log(Path.Combine(saveImagesPath, gameObject.name + ".png"));

            }
            else
            {
                if (!ObjectStatisticsUtility.HasRender(gameObject))
                    return;

                gameObject.GetComponent<Renderer>().material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
                Debug.Log("adding new texture");
            
            }
        }
    }

    // Saves all the images on game object that can be painted (in array of object gotton from PrepairWorld)
    private void SaveImages()
    {
        foreach (GameObject gameObject in PrepairWorld.paintableObjects)
        {
            if (!ObjectStatisticsUtility.HasMainTexture(gameObject))
                return;
            
            Texture2D image = (Texture2D) gameObject.GetComponent<Renderer>().material.mainTexture;
            File.WriteAllBytes(Path.Combine(saveImagesPath, gameObject.name + ".png"), image.EncodeToPNG());
        }
    }

}
