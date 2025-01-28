using System;
using UnityEngine;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

/**************************************************
 * Attached to: SaveManager
 * Purpose: saves and loads the texture you paint on 
 * Author: Seamus
 * Version: 1.0
 *************************************************/

[ExecuteInEditMode]
public class SaveLoadImagesManager : MonoBehaviour
{
    String saveImagesPath;     
    private TusInputAction paintAction;
    public static float texelDensity = 2000;
    public static GameObject[] PaintableObjects { get; private set; }


    private void Awake()
    {
        paintAction = new TusInputAction();
    }

    private void Start() 
    {
        paintAction.Enable();
        paintAction.DominantArm_RightHanded.Save.performed += ctx => SaveImages();

        AddToArrayAllPaintableObjects();
        //CreateNewBlankTexture();

        saveImagesPath = Application.persistentDataPath;// use when building for Quest
        //saveImagesPath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave"; // use when testing with pc on Seamus computer 

        LoadImages();
    }

    private void OnDestroy() 
    {
        SaveImages();
    }


    // Load all the images from drive onto the paintableObjects 
    private void LoadImages()
    {
        // Goes over all objects in PaintableObjects
        foreach (GameObject gameObject in PaintableObjects)
        {

            if (File.Exists(Path.Combine(saveImagesPath, gameObject.name + ".png"))) // Check the game has already saved and the file exists
            {
                // Load the images for disk
                byte[] imageData = File.ReadAllBytes(Path.Combine(saveImagesPath, gameObject.name + ".png"));

                // Converts to a Texture2D
                Texture2D objectTexture = new Texture2D(2,2);
                ImageConversion.LoadImage(objectTexture, imageData);

                // Sets the the loaded texture to the object's texture
                gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = objectTexture;
                //Debug.Log(Path.Combine(saveImagesPath, gameObject.name + ".png"));

            } 
            else // The texture does not exist 
            {
                if (!ObjectStatisticsUtility.HasRender(gameObject))
                    return;
                
                // Creates a new texture
                gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
            }
        }
    }
    

    // private void LoadImagesParallel()
    // {
    //     String[] names = new String[PaintableObjects.Length];
    //     Texture[] textures = new Texture[PaintableObjects.Length];
    //     Mesh[] meshes = new Mesh[PaintableObjects.Length];
        
    //     for (int i = 0; i < PaintableObjects.Length; i++)
    //     {
    //         names[i] = PaintableObjects[i].name;
    //         meshes[i] = PaintableObjects[i].GetComponent<MeshFilter>().mesh;
    //     }

    //     Parallel.For(0, PaintableObjects.Length, i => 
    //     {
    //         if (File.Exists(Path.Combine(saveImagesPath, names[i] + ".png"))) // Check the game has already saved and the file exists
    //         {
    //             // Load the images for disk
    //             byte[] imageData = File.ReadAllBytes(Path.Combine(saveImagesPath, names[i] + ".png"));

    //             // Converts to a Texture2D
    //             Texture2D objectTexture = new Texture2D(2,2);
    //             ImageConversion.LoadImage(objectTexture, imageData);

    //             textures[i] = objectTexture;

    //         } 
    //         else // The texture does not exist 
    //         {
    //             textures[i] = ObjectStatisticsUtility.CreateObjectTexture(meshes[i], texelDensity);
    //         }
    //     });

    //     for (int i = 0; i < PaintableObjects.Length; i++)
    //     {
    //         PaintableObjects[i].GetComponent<Renderer>().material.mainTexture = textures[i];
    //         Debug.Log(textures[i].dimension);
    //     }
    // }

    // TODO make async https://discussions.unity.com/t/save-rendertexture-or-texture2d-as-image-file-utility/891718/14 
    // Saves all the images on game object that can be painted (in array of object gotten from PrepareWorld)
    private void SaveImages()
    {
        // Goes over all objects in paintableObjects
        foreach (GameObject gameObject in PaintableObjects)
        {
            if (!ObjectStatisticsUtility.HasMainTexture(gameObject))
                return;
            
            // Saves the images to disk 
            Texture2D image = (Texture2D) gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture;
            File.WriteAllBytes(Path.Combine(saveImagesPath, gameObject.name + ".png"), image.EncodeToPNG());
        }
    }

    // Gets all active GameObject that have Renderer component 
    public GameObject[] AddToArrayAllPaintableObjects()
    {
        GameObject[] gameObjects;

        gameObjects = GameObject.FindObjectsOfType<GameObject>()
                                .Where(go => go.layer == 6).ToArray()
                                .Where(go => ObjectStatisticsUtility.HasRender(go)).ToArray();

        PaintableObjects = gameObjects;
        return gameObjects;
    }

    // This does not need explication 
    public void CreateNewBlankTexture()
    {
        foreach (GameObject gameObject in PaintableObjects)
        {
            gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
        }
    }


    public void SetPaintAbleObjectFields(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponent<PaintableObject>() == null)
                continue;
                
            gameObject.GetComponent<PaintableObject>().surfaceArea = ObjectStatisticsUtility.CalculateObjectArea(gameObject);
            gameObject.GetComponent<PaintableObject>().uvRatio = ObjectStatisticsUtility.CalculateObjectUVAreaRatio(gameObject);

        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SaveLoadImagesManager))]
class SerializedSaveLoadImagesManager : Editor
{
    public override void OnInspectorGUI()
    {
        SaveLoadImagesManager SLIM = target as SaveLoadImagesManager;
        base.OnInspectorGUI();

        if (GUILayout.Button("Calculate Paintable Fields"))
        {
            GameObject[] gameObjects = SLIM.AddToArrayAllPaintableObjects();

            SLIM.SetPaintAbleObjectFields(gameObjects);
        }

        if (GUILayout.Button("Reset Textures"))
        {
            SLIM.CreateNewBlankTexture();
        }
    }
}
#endif
