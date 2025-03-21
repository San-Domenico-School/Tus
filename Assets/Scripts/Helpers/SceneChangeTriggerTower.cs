using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Tuan Le
//1/28/2025
//Different Door Triggers
public class SceneChangeTriggerTower : MonoBehaviour
{

    [SerializeField] private int sceneNum;
    private SceneTracker sceneTracker;

    private void OnEnable()
    {
        sceneTracker = GameObject.FindWithTag("SceneManager").GetComponent<SceneTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player triggered the zone
        if (other.gameObject.CompareTag("Player"))
        {
            sceneTracker.LoadScene(sceneNum); // Call the ChangeScene method in SceneTracker
        }
    }
}
