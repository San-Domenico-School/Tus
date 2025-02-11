using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comments : MonoBehaviour
{
    [TextArea(3, 10)]
    public string comment = "DO NOT CHANGE THIS SCRIPT\n" +
                            "this script is essential for the SceneManagers workings." +
                            "the SerializeField sceneTracker should hold the game object " +
                            "SceneManager. it should automaticly navigate to " +
                            "the sceneTracker script, but if it doesnt than idk come to me.";
}