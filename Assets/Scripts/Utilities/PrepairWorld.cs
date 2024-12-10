using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/**************************************************
 * Attached to: PrepairWorld 
 * Purpose: add or create a texture for all objects that will be painted
 * Author: Seamus 
 * Version: 1.0
 ██████▄░▄█████▄░██████▄░████████░░░██░░░██░▄██████░▄█████░░░████████░██░░░██░██████░▄██████░░░
██░░░██░██░░░██░██░░░██░░░░██░░░░░░██░░░██░██░░░░░░██░░░░░░░░░░██░░░░██░░░██░░░██░░░██░░░░░░░░
██░░░██░██░░░██░██░░░██░░░░██░░░░░░██░░░██░▀█████▄░█████░░░░░░░██░░░░███████░░░██░░░▀█████▄░░░
██░░░██░██░░░██░██░░░██░░░░██░░░░░░██░░░██░░░░░░██░██░░░░░░░░░░██░░░░██░░░██░░░██░░░░░░░░██░░░
██████▀░▀█████▀░██░░░██░░░░██░░░░░░▀█████▀░██████▀░▀█████░░░░░░██░░░░██░░░██░██████░██████▀░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
 *************************************************/

public class PrepairWorld : MonoBehaviour
{
    [SerializeField] bool precomputeTextures = false;
    public static float texelDensity = 20;
    public static GameObject[] paintableObjects { get; private set; }


    private void Awake() 
    {
        GetAllPainableObjects();

        if (precomputeTextures)
        {
            AddTextureToObjects();
        }

    }

    private void GetAllPainableObjects()
    {
        // gets all acive GameObject that have mainTextures
        paintableObjects = GameObject.FindObjectsOfType<GameObject>().Where(go => go.activeSelf).ToArray().Where(go => ObjectStatisticsUtility.HasRender(go)).ToArray();

    }

    private void AddTextureToObjects()
    {
        foreach (GameObject gameObject in paintableObjects)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = ObjectStatisticsUtility.CreateObjectTexture(gameObject, texelDensity);
        }
    }



    // private void Start() {

    //     AddTextureToObjectsParallelly();

    //     List<GameObject> allObjects = new List<GameObject>();

    //     foreach (GameObject paintableObject in paintableObjects) 
    //     {
    //         allObjects.Add(getAllChildObjects(paintableObject)[0]);
    //     }
    //     allPaintalbeObjects = allObjects.ToArray();


    //     foreach (GameObject obj in allPaintalbeObjects)
    //     {
    //         Debug.Log(obj.name );
    //     }
    // }

//
    // private GameObject[] getAllChildObjects(GameObject gameObject)
    // {
    //     if (gameObject.transform.childCount <= 0)
    //     {
    //         return new GameObject[1] {gameObject};
    //     }
    //     else
    //     {
    //         List<GameObject> childObjects = new List<GameObject>();

    //         for (int i = 0; i < gameObject.transform.childCount; i++)
    //         {
    //             childObjects.Add(getAllChildObjects(gameObject.transform.GetChild(i).gameObject)[0]);

    //         }
    //         return childObjects.ToArray();
    //     }
    // }

}