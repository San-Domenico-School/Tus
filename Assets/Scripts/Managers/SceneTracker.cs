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
    // Array to store spawn locations for each scene
    private Vector3[] spawnLocations = new Vector3[6];

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

          //  Debug.Log("Scene {nextSceneIndex} is now available!");
        }
    }

    // Load the appropriate scene based on the current scene index.
    public void ChangeScene()
    {
        Debug.Log("chnage scene called");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            LoadScene(1); // From tutorial to scene 1
        }
        else if (currentSceneIndex >= 1 && currentSceneIndex <= 4)
        {
            LoadScene(5); // From scenes 1-4 to the tower
        }
    }

    // Loads a scene and moves the player to the correct spawn position.
    private void LoadScene(int sceneIndex)
    {
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
        int sceneIndex = scene.buildIndex;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnLocations[sceneIndex];
            Debug.Log($"Player moved to spawn position for scene {sceneIndex}: {spawnLocations[sceneIndex]}");
        }
        else
        {
            Debug.LogWarning("Player object not found in the scene.");
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
