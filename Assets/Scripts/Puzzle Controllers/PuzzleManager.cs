using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is the parent for a given puzzle and its blocks
/// it will be responsible for managing the puzzle, its blocks, and the player's progress
/// Teddy Fleitas
/// December 21, 2024
/// most recent update as of 4/23/25
/// </summary>

public class PuzzleManager : MonoBehaviour
{
    private GameObject referenceBlock;
    private GameObject playerBlock;
    private float accuracyThreshold = 0.99f;  //0.20f = 80% accuracy required to complete the puzzle
    public bool isMatched = false;  //holds the result of the color comparison
    [SerializeField] private SceneTracker sceneTracker;
    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        referenceBlock = GameObject.Find("referenceBlock");
        playerBlock = GameObject.Find("playerBlock");

        isMatched = CheckColorMatch(referenceBlock, playerBlock, accuracyThreshold); //sets isatched

        if (referenceBlock != null)
        {
            // Set the referenceBlock's texture to a random RGB value
            Color randomColor = new Color(Random.value, Random.value, Random.value); //makes random RGB values
            referenceBlock.GetComponent<Renderer>().material.color = randomColor; //sets referenceBlock's color to randomColor
        }
        else
        {
            Debug.LogError("Reference block not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (referenceBlock != null && playerBlock != null)
        {
            isMatched = CheckColorMatch(referenceBlock, playerBlock, accuracyThreshold);
        }
    }

    bool CheckColorMatch(GameObject referenceBlock, GameObject playerBlock, float accuracyThreshold)
    {
        //get the color of the reference block
        Color referenceColor = referenceBlock.GetComponent<Renderer>().material.color;
        //get the color of the player block
        Color playerColor = playerBlock.GetComponent<Renderer>().material.color;
        //compare the colors
        float redAccuracy = Mathf.Abs(referenceColor.r - playerColor.r);
        float greenAccuracy = Mathf.Abs(referenceColor.g - playerColor.g);
        float blueAccuracy = Mathf.Abs(referenceColor.b - playerColor.b);
        //check if the colors are within the threshold
        if (redAccuracy <= accuracyThreshold && greenAccuracy <= accuracyThreshold && blueAccuracy <= accuracyThreshold)
        {
            /*
            //use for debugging
            Debug.Log($"Reference Block RGB: ({referenceColor.r}, {referenceColor.g}, {referenceColor.b})");
            Debug.Log($"Player Block RGB: ({playerColor.r}, {playerColor.g}, {playerColor.b})");
            Debug.Log("Matched");
            */

            sceneTracker.UnlockNextScene(0);
            door.gameObject.SetActive(false);
            isMatched = true;
            return true;
        }
        else
        {
            //Debug.Log("Not Matched"); //debugging
            isMatched = false;
            return false;
        }
    }
}

