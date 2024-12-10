using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/**************************************************
 * Attached to: SaveManager
 * Purpose: saves and loads the texture you paint on 
 * Author: Seamus
 * Version: 1.0
 *************************************************/

public class SaveLoadImagesManager : MonoBehaviour
{
    String saveImagesPath;     
    private TusInputAction paintAction;
    public static float texelDensity = 20;
    public static GameObject[] paintableObjects { get; private set; }


    private void Awake()
    {
        paintAction = new TusInputAction();
    }

    private void Start() 
    {
        AddToArrayAllPaintableObjects();

        paintAction.Enable();
        paintAction.DominantArm_RightHanded.Save.performed += ctx => SaveImages();


        saveImagesPath = Application.persistentDataPath;// use when building for Quest
        //saveImagesPath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave"; // use when tesing with pc on Seamus computer 

        LoadImages();
    }



    private void OnDestroy() 
    {
        SaveImages();
    }

    // Load all the images onto the game objects 
    private void LoadImages()
    {

        float texelDensity = SaveLoadImagesManager.texelDensity;

        foreach (GameObject gameObject in SaveLoadImagesManager.paintableObjects)
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
            
            }
        }
    }

    // TODO make async https://discussions.unity.com/t/save-rendertexture-or-texture2d-as-image-file-utility/891718/14 
    // Saves all the images on game object that can be painted (in array of object gotton from PrepairWorld)
    private void SaveImages()
    {
        Debug.Log("save image");
        foreach (GameObject gameObject in PrepairWorld.paintableObjects)
        {
            if (!ObjectStatisticsUtility.HasMainTexture(gameObject))
                return;
            
            Texture2D image = (Texture2D) gameObject.GetComponent<Renderer>().material.mainTexture;
            File.WriteAllBytes(Path.Combine(saveImagesPath, gameObject.name + ".png"), image.EncodeToPNG());
        }
    }

    private void AddToArrayAllPaintableObjects()
    {
        // gets all active GameObject that have mainTextures
        paintableObjects = GameObject.FindObjectsOfType<GameObject>().Where(go => go.activeSelf).ToArray().Where(go => ObjectStatisticsUtility.HasRender(go)).ToArray();

    }

    private void CreateNewBlankTexture()
    {
        foreach (GameObject gameObject in paintableObjects)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
        }
    }

}
