using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Tuan Le
//1/28/2025
//Open up doors to other scene
public class PathManager : MonoBehaviour
{
    
    [SerializeField] Transform platformPath;
    [SerializeField] Transform forestDoor;
    [SerializeField] Transform caveDoor;
    [SerializeField] Transform skyDoor;
    [SerializeField] Transform canyonDoor;
    [SerializeField] Transform altar;
    private SceneTracker sceneTracker;
    private GameObject sceneManager;
    
    
    // Get the SceneTracker for doors
    void OnEnable()
    {
        sceneManager = GameObject.Find("SceneManager");
        sceneTracker = sceneManager.GetComponent<SceneTracker>();
    }

    // Update is called once per frame  
    //Check for each Door to open and Platform to move up
    void Update()
    {
        Altair altarScript = altar.transform.GetComponent<Altair>();
        // sceneTracker = GameObject.FindWithTag("SceneManager").GetComponent<SceneTracker>();

        if(altarScript.inProx)
        {
            Platform platformScript = platformPath.transform.GetComponent<Platform>();
            platformScript.ActivatePath();
        }
        if(sceneTracker.sceneAvailable[1])
        {
            Platform forestScript = forestDoor.transform.GetComponent<Platform>();
            forestScript.ActivatePath();
        }
        if(sceneTracker.sceneAvailable[2])
        {
            Platform caveScript = caveDoor.transform.GetComponent<Platform>();
            caveScript.ActivatePath();
        }
        if(sceneTracker.sceneAvailable[3])
        {
            Platform skyScript = skyDoor.transform.GetComponent<Platform>();
            skyScript.ActivatePath();
        }
        if(sceneTracker.sceneAvailable[4])
        {
            Platform canyonScript = canyonDoor.transform.GetComponent<Platform>();
            canyonScript.ActivatePath();
        }
    }
}
