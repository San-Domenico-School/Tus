using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneMove : MonoBehaviour
{
    private SceneTracker sceneTracker;

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
