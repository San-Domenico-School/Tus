using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/**************************************************
 * Attached to: Simon puzzle empty gameobject
 * Purpose: simon puzzle interact / game
 * Author: reece/teddy
 * Version: 1.3
 *************************************************/

public class TeddySimonVersion : MonoBehaviour
{
    [SerializeField] public GameObject fromObject;
    [SerializeField] public float rayMaxDistance = 30f;
    [SerializeField] public GameObject DisplayBlock;
    [SerializeField] public GameObject DoorPart;
    [SerializeField] public SceneTracker sceneTracker;
    //[SerializeField] private GameObject testBlock;

    public Material red;
    public Material blue;
    public Material green;
    public Material yellow;
    public Material black;
    public Material loseColor;
    public Material winColor;

    public bool puzzleCompleted = false;

    private bool runningDisplay = false;
    private int actionCount = 0;
    private int displayCounter = 0;

    //private TusInputAction controls;

    private List<string> gameActions = new List<string>();
    private List<string> madeActions = new List<string>();

    //GameObject ColorDisplay = GameObject.Find("PuzzleColor");

    private Renderer objectRenderer;
    private MeshRenderer doorRenderer;
    private Collider doorCollider;

    // Start is called before the first frame update

    private void Awake()
    {
        //controls = new TusInputAction();
    }


    void OnEnable()
    {
        //controls.Enable();

        CreateSimonOrder();

        //controls.DominantArm_RightHanded.ObjectInteract.performed += ctx => Interact()

        objectRenderer = DisplayBlock.GetComponent<Renderer>();
        doorRenderer = DoorPart.GetComponent<MeshRenderer>();
        doorCollider = DoorPart.GetComponent<Collider>();
    }

    private void CreateSimonOrder()
    {
        for (int i = 1; i < 5; i++)
        {
            int randomNumber = Random.Range(1, 4);

            if (randomNumber == 1)
            {
                gameActions.Add("Red");
            }
            else if (randomNumber == 2)
            {
                gameActions.Add("Blue");
            }
            else if (randomNumber == 3)
            {
                gameActions.Add("Green");
            }
            else if (randomNumber == 4)
            {
                gameActions.Add("Yellow");
            }
        }
    }

    void Start()
    {
        sceneTracker = GameObject.Find("SceneManager").GetComponent<SceneTracker>();
        fromObject = GameObject.Find("Left Hand");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Dynamic input action methods
    public void OnPuzzleClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) Interact();
    }

    void DisplayChallenge() // display colors that they need to hit in sequence
    {
        ;
        runningDisplay = true;
        // 0: color one 1: black 2: color two 3: black 4: color three 5: black
        // 6: color four 7: black

        string color;

        if (displayCounter == 0)
        {
            color = gameActions[0];
        }
        else if (displayCounter == 2)
        {
            color = gameActions[1];
        }
        else if (displayCounter == 4)
        {
            color = gameActions[2];
        }
        else if (displayCounter == 6)
        {
            color = gameActions[3];
        }
        else
        {
            color = "Black";
        }

        //GameObject markerBlock = Instantiate(testBlock);

        //markerBlock.transform.position = new Vector3(1, 4 + (displayCounter), 1);
        //markerBlock.transform.localScale = new Vector3(1, 1, 3);

        if (color == "Red")
        {
            objectRenderer.material = red;
            //markerBlock.GetComponent<Renderer>().material = red;
        }
        else if (color == "Blue")
        {
            objectRenderer.material = blue;
            //markerBlock.GetComponent<Renderer>().material = blue;
        }
        else if (color == "Green")
        {
            objectRenderer.material = green;
            //markerBlock.GetComponent<Renderer>().material = green;
        }
        else if (color == "Yellow")
        {
            objectRenderer.material = yellow;
            //markerBlock.GetComponent<Renderer>().material = yellow;
        }
        else if (color == "Black")
        {
            objectRenderer.material = black;
            //markerBlock.GetComponent<Renderer>().material = black;
        }

        //Destroy(markerBlock, 1);

        displayCounter++;

        if (displayCounter == 8)
        {
            objectRenderer.material = black;
            runningDisplay = false;
            CancelInvoke();
        }

    }

    public void Win()
    {
        objectRenderer.material = winColor;
        puzzleCompleted = true;
        sceneTracker.UnlockNextScene(1);

        doorRenderer.enabled = false;
        doorCollider.enabled = false;
        Debug.Log("win method ran");
    }

    public void Lose()
    {
        objectRenderer.material = loseColor;
        madeActions = new List<string>();
    }

    public void Interact()
    {
        if (puzzleCompleted == false)
        {
            Debug.Log("Simon interact started");

            RaycastHit hit;
            Physics.Raycast(fromObject.transform.position, fromObject.transform.forward, out hit, rayMaxDistance);

            if (hit.transform == null)
                return;

            //GameObject markerBlock = Instantiate(testBlock);

            //markerBlock.transform.position = new Vector3(8 + (actionCount), 4, 4);
            //markerBlock.transform.localScale = new Vector3(1, 1, 1);

            actionCount += 1;

            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.gameObject.name == "SimonRed")
            {
                //markerBlock.GetComponent<Renderer>().material = red;

                if (gameActions[madeActions.Count] == "Red")
                {
                    madeActions.Add("Red");
                }
                else
                {
                    Lose();
                }
            }
            else if (hit.transform.gameObject.name == "SimonBlue")
            {
                //markerBlock.GetComponent<Renderer>().material = blue;

                if (gameActions[madeActions.Count] == "Blue")
                {
                    madeActions.Add("Blue");

                }
                else
                {
                    Lose();
                }
            }
            else if (hit.transform.gameObject.name == "SimonGreen")
            {
                //markerBlock.GetComponent<Renderer>().material = green;

                if (gameActions[madeActions.Count] == "Green")
                {
                    madeActions.Add("Green");

                }
                else
                {
                    Lose();
                }
            }
            else if (hit.transform.gameObject.name == "SimonYellow")
            {
                //markerBlock.GetComponent<Renderer>().material = yellow;

                if (gameActions[madeActions.Count] == "Yellow")
                {
                    madeActions.Add("Yellow");

                }
                else
                {
                    Lose();
                }
            }
            else if (hit.transform.gameObject.name == "SimonDisplay")
            {
                if (runningDisplay == false)
                {
                    madeActions = new List<string>();
                    //markerBlock.GetComponent<Renderer>().material = black;

                    displayCounter = 0;
                    InvokeRepeating("DisplayChallenge", 0, 1);
                }
            }

            if (madeActions.Count == gameActions.Count)
            {
                //objectRenderer.material = green;
                Win();
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(TeddySimonVersion))]

class SerializedTeddySimonVersion : Editor
{
    public override void OnInspectorGUI()
    {
        TeddySimonVersion SLIM = target as TeddySimonVersion;
        base.OnInspectorGUI();

        if (GUILayout.Button("Run Win Method"))
        {
            SLIM.Win();
        }
    }
}
#endif