using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneMove : MonoBehaviour
{
    /***********************
     * This script is on the sceneChangeTrigger
     * and tells the sceneManager when to 
     * change the current scene loaded.
     * This gameobject should also have a trigger attached to it 
     * Teddy F 2/14/25
     **********************/

    [SerializeField] private SceneTracker sceneTracker;

    private void OnEnable()
    {
        sceneTracker = GameObject.FindWithTag("SceneManager").GetComponent<SceneTracker>();
        Debug.Log("onEnable Called");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player triggered the zone
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            SceneTracker.Instance.ChangeScene(); // Call the ChangeScene method in SceneTracker
            Debug.Log("colision check");
        }
    }
}
