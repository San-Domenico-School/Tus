using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate prefab
            return;
        }

        // Assign this as the instance
        instance = this;

        // Make the parent (and its children) persistent across scenes
        DontDestroyOnLoad(gameObject);
    }
}
