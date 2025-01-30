using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    /**************************
     *This script is on the SceneTracker. it keeps  
     *track of what levels are unlocked based of 
     *previous puzzle completion. It is DDOL and is
     *in control of changing the scene
     *
     *Teddy Fleitas
     *************************/

    public static SceneTracker Instance;
    // Array to track available scenes
    private bool[] sceneAvailable = new bool[6] { true, false, false, false, false, true };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Unlock the next scene after completing the current scene's puzzle.    
    public void UnlockNextScene(int currentSceneIndex)
    {
        if (currentSceneIndex >= 0 && currentSceneIndex < 5)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            sceneAvailable[nextSceneIndex] = true;

          //Debug.Log("Scene {nextSceneIndex} is now available!");
        }
    }

    // Load the appropriate scene based on the current scene index.
    public void ChangeScene()
    {
        Debug.Log("chanage scene called");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            gameObject.SetActive(false);
            LoadScene(1); // tutorial to scene 1
            gameObject.SetActive(true);
        }
        else if (currentSceneIndex >= 1 && currentSceneIndex <= 4)
        {
            LoadScene(5); // scenes 1-4 to the tower
        }
    }

    // Loads a scene and moves the player to the correct spawn position.
    private void LoadScene(int sceneIndex)
    {
        Debug.Log("load scene called");
        //load scene called
        if (sceneIndex >= 0 && sceneIndex < sceneAvailable.Length && sceneAvailable[sceneIndex])
        {
            SceneManager.LoadScene(sceneIndex);
            
            // Move player to the correct spawn location
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        else
        {
            Debug.LogWarning($"Scene {sceneIndex} is not available or out of range.");
        }
    }


    // Callback to position the player after a scene is loaded.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawnPoint = GameObject.Find("PlayerSpawnpoint");

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            Debug.Log($"Player moved to spawn point at {spawnPoint.transform.position:F6}");
        }
        else
        {
            if (player == null)
                Debug.LogWarning("Player not found in the scene.");
            if (spawnPoint == null)
                Debug.LogWarning("Spawnpoint not found in the scene.");
        }

        SceneManager.sceneLoaded -= OnSceneLoaded; // Remove event listener to prevent duplication.
    }
}