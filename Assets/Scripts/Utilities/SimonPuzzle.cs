using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**************************************************
 * Attached to: idk
 * Purpose: simon puzzle interact / game
 * Author: reece
 * Version: 1.0
 *************************************************/

public class SimonPuzzle : MonoBehaviour
{
    [SerializeField] GameObject fromObject;
    [SerializeField] float rayMaxDistance = 30f;

    public Material red;
    public Material blue;
    public Material green;
    public Material yellow;
    public Material black;

    private TusInputAction controls;

    private List<string> gameActions = new List<string>();
    private List<string> madeActions = new List<string>();

    //GameObject ColorDisplay = GameObject.Find("PuzzleColor");

    Renderer objectRenderer;

    // Start is called before the first frame update

    private void Awake()
    {
        controls = new TusInputAction();
    }

    void OnEnable()
    {
        controls.Enable();

        controls.DominantArm_RightHanded.ObjectInteract.performed += ctx => Interact();

        gameActions.Add("Red");
        gameActions.Add("Green");
        gameActions.Add("Blue");
        gameActions.Add("Yellow");

        objectRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait(int length)
    {
        yield return new WaitForSeconds(length);
    }

    void DisplayChallenge() // display colors that they need to hit in sequence
    {

        foreach (string color in gameActions)
        {
            if (color == "Red")
            {
                objectRenderer.material = red;
            }
            else if (color == "Blue")
            {
                objectRenderer.material = blue;
            }
            else if (color == "Green")
            {
                objectRenderer.material = green;
            }
            else if (color == "Yellow")
            {
                objectRenderer.material = yellow;
            }

            Wait(1);
        }

        objectRenderer.material = black;
    }

    void Interact()
    {
        Debug.Log("Simon interact started");

        RaycastHit hit;
        Physics.Raycast(fromObject.transform.position, fromObject.transform.forward, out hit, rayMaxDistance);

        if (hit.transform == null)
            return;

        Debug.Log(hit.transform.gameObject.name);

        if (hit.transform.gameObject.name == "SimonRed")
        {
            if (gameActions[madeActions.Count] == "Red")
            {
                madeActions.Add("Red");
            }
        }
        else if (hit.transform.gameObject.name == "SimonBlue")
        {
            if (gameActions[madeActions.Count] == "Blue")
            {
                madeActions.Add("Blue");
            }
        }
        else if (hit.transform.gameObject.name == "SimonGreen")
        {
            if (gameActions[madeActions.Count] == "Green")
            {
                madeActions.Add("Green");
            }
        }
        else if (hit.transform.gameObject.name == "SimonYellow")
        {
            if (gameActions[madeActions.Count] == "Yellow")
            {
                madeActions.Add("Yellow");
            }
        }
        else if (hit.transform.gameObject.name == "PuzzleColor")
        {
            DisplayChallenge();
        }
        else
        {
            objectRenderer.material = red;
            Debug.Log("Loss");
        }

        if (madeActions.Count == gameActions.Count)
        {
            objectRenderer.material = green;
            Debug.Log("Win");
        }
    }
}
