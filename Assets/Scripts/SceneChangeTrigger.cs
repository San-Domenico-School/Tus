using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneMove : MonoBehaviour
{
    //[SerializeField] private SceneTracker sceneTracker;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player triggered the zone
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            SceneTracker.Instance.ChangeScene(); // Call the ChangeScene method in SceneTracker
        }
    }
}
