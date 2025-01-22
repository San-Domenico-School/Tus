using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneMove : MonoBehaviour
{
    [SerializeField] private SceneTracker sceneTracker;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player triggered the zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            if (sceneTracker != null)
            {
                sceneTracker.ChangeScene(); // Call the ChangeScene method in SceneTracker
            }
            else
            {
                Debug.LogWarning("SceneTracker is not assigned. Scene transition cannot occur.");
            }
        }
    }
}
