using System;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

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
    public static float texelDensity = 10;
    public static GameObject[] PaintableObjects { get; private set; }


    private void Awake()
    {
        AddToArrayAllPaintableObjects();
        paintAction = new TusInputAction();
    }

    private void Start() 
    {

        paintAction.Enable();
        paintAction.NonDominantArm_LeftHanded.Save.performed += ctx => SaveImages();
        paintAction.NonDominantArm_LeftHanded.Clear.performed += ctx => CreateNewBlankTexture();

        //CreateNewBlankTexture();

        saveImagesPath = Application.persistentDataPath;// use when building for Quest
        //saveImagesPath = "C:/Users/happy/OneDrive/Documents/School Projects/Tus/Unity/Tus/Assets/Scripts/SaveLoad/TestSave"; // use when testing with pc on Seamus computer 

        if (Application.isPlaying)
        {
            LoadImages();
        }
    }

    private void OnDestroy() 
    {
        SaveImages();
    }


    // Load all the images from drive onto the paintableObjects 
    private void LoadImages()
    {
        if (PaintableObjects == null) 
            return;

        foreach (GameObject gameObject in PaintableObjects)
        {
            Texture2D texture2D;

            if (File.Exists(Path.Combine(saveImagesPath, GetObjectsImageFileName(gameObject.transform)))) // Check the game has already saved and the file exists
            {
                // Load the images for disk
                byte[] imageData = File.ReadAllBytes(Path.Combine(saveImagesPath, GetObjectsImageFileName(gameObject.transform)));

                // Converts to a Texture2D
                Texture2D objectTexture = new Texture2D(2,2);
                ImageConversion.LoadImage(objectTexture, imageData);

                texture2D = objectTexture;
                //Debug.Log(Path.Combine(saveImagesPath, GetObjectsImageFileName(gameObject)));
            } 
            else // The texture does not exist 
            {
                if (!ObjectStatisticsUtility.HasRender(gameObject))
                    return;
                
                // Creates a new texture
                texture2D = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
            }

            RenderTexture paintRT = new RenderTexture(texture2D.width, texture2D.height, 0, RenderTextureFormat.ARGB32);
            paintRT.Create();

            RenderTexture active = RenderTexture.active;
            RenderTexture.active = paintRT;
            GL.Clear(true, true, Color.gray);
            Graphics.Blit(texture2D, paintRT);
            RenderTexture.active = active;

            gameObject.GetComponent<MeshRenderer>().material.mainTexture = paintRT;
            gameObject.GetComponent<PaintableObject>().paintRT = paintRT;
            
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
    //         PaintableObjects[i].GetComponent<Renderer>().sharedMaterial.mainTexture = textures[i];
    //         Debug.Log(textures[i].dimension);
    //     }
    // }

    // TODO make async https://discussions.unity.com/t/save-rendertexture-or-texture2d-as-image-file-utility/891718/14 
    // Saves all the images on game object that can be painted (in array of object gotten from PrepareWorld)
    private void SaveImages()
    {
        if (PaintableObjects == null) 
            return;
            
        // Goes over all objects in paintableObjects
        foreach (GameObject gameObject in PaintableObjects)
        {
            if (!ObjectStatisticsUtility.HasMainTexture(gameObject))
                return;
            
            // Saves the images to disk 
            Texture2D image =  ToTexture2D((RenderTexture) gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture);
            File.WriteAllBytes(Path.Combine(saveImagesPath, GetObjectsImageFileName(gameObject.transform)), image.EncodeToPNG());
        }
    }

    private Texture2D ToTexture2D(RenderTexture renderTexture)
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        return texture;
    }

    // Gets all active GameObject that have Renderer component 
    public void AddToArrayAllPaintableObjects()
    {
        PaintableObjects = GameObject.FindObjectsOfType<GameObject>().Where(go => ObjectStatisticsUtility.IsPaintable(go)).ToArray();
    }

    // This does not need explication 
    public void CreateNewBlankTexture()
    {
        foreach (GameObject gameObject in PaintableObjects)
        {
            gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
        }
        SaveImages();
    }


    public void SetPaintAbleObjectFields()
    {
        foreach (GameObject gameObject in PaintableObjects)
        {
            if (gameObject.GetComponent<PaintableObject>() == null)
                continue;
                
            gameObject.GetComponent<PaintableObject>().surfaceArea = ObjectStatisticsUtility.CalculateObjectArea(gameObject);
            gameObject.GetComponent<PaintableObject>().uvRatio = ObjectStatisticsUtility.CalculateObjectUVAreaRatio(gameObject);

        }
    }

    private string GetObjectsImageFileName(Transform transform)
    {
        string name = SceneManager.GetActiveScene().name;

        while (transform != null)
        {
            name += transform.name; 
            transform = transform.parent;
        }
        name += ".png";
        // Debug.Log(Path.Combine(saveImagesPath, name));

        return name;
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
            SLIM.AddToArrayAllPaintableObjects();

            SLIM.SetPaintAbleObjectFields();
        }

        if (GUILayout.Button("Reset Textures"))
        {
            SLIM.CreateNewBlankTexture();
        }
    }
}
#endif
