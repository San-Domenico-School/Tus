using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**************************************************
 * Attached to: paint manager 
 * Purpose: shoot a ray out and paint the world at its location
 * Author: Seamus/Teddy
 * Version: 1.2
 *************************************************/

public class Painter : MonoBehaviour
{

    public static Painter Instance;
    private TusInputAction paintAction;
    private bool isPainting;
    private GameObject fromObject;

    [SerializeField] Texture2D brush;
    [SerializeField] public Color paintColor = Color.white;
    [SerializeField] float rayMaxDistance = 30f;
    [SerializeField] float brushSize;

    //public float paintRemaining { get; set; } = 50;
    public float paintRemaining = 500;


    private void Awake()
    {
        paintAction = new TusInputAction();
        brushSize = PaintSizer.Instance.GetCurrentSize();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        paintAction.Enable();
        paintAction.DominantArm_RightHanded.Paint.performed += ctx => isPainting = true; 
        paintAction.DominantArm_RightHanded.Paint.canceled += ctx => isPainting = false;
        fromObject = GameObject.Find("Right hand");
    }

    private void Update()
    {
        HandlePainting();
    }


    public Color GetPaintColor()
    {
        return paintColor;
    }
    public void SetPaintColor(Color color)
    {
        paintColor = color;
    }

    // Called every update. Check if you are painting and if you have paint 
    // Remove paint from paintRemaining
    private void HandlePainting()
    {
        if (isPainting)
        {
            if (paintRemaining >= 0)
            {
                PaintObject();
                paintRemaining -= Time.deltaTime;

                //Debug.Log(paintRemaining);
            }
            else
            {
                Debug.Log("PAINT RAN OUT!!!");
            }
        }
    }

    // Shoot a ray where the paint will be
    private void PaintObject()
    {
        // Shoots ray for fromObject forward
        RaycastHit hit;
        Physics.Raycast(fromObject.transform.position, fromObject.transform.forward, out hit, rayMaxDistance); 

        // Checks if it has a transform, is on Paintable layer, has a render
        if (hit.transform == null || !(hit.transform.gameObject.layer == 6) || !ObjectStatisticsUtility.HasRender(hit.transform.gameObject))
            return;


        Texture2D texture = ObjectStatisticsUtility.GetOrCreateObjectsTexture(hit.transform.gameObject, SaveLoadImagesManager.texelDensity);

        PaintTexture(hit.textureCoord, texture);
    }

    //paints the texture at the UV cordate with diameter of the brushSize and shape of brush 
    private void PaintTexture(Vector2 uv, Texture2D texture)
    {
        // UV to pixels 
        uv.x *= texture.width;
            uv.y *= texture.height;

        // Calculate brushWidth
        int brushWidth = (int)(brush.width * brushSize);
        int brushHeight = (int)(brush.height * brushSize);

        //paints the paintColor where the r value of the brush is 255
        for (int x = 0; x < brushWidth; x++)
        {
            for (int y = 0; y < brushHeight; y++)
            {
                int currentTextureX = (int)(uv.x + x - (brushWidth / 2));
                int currentTextureY = (int)(uv.y + y - (brushHeight / 2));

                Color brushColor = brush.GetPixel((int)(x / brushSize), (int)(y / brushSize));
                brushColor = Color.Lerp(texture.GetPixel(currentTextureX, currentTextureY), paintColor, brushColor.r);

                //brushColor = brush.GetPixel((int)(x / brushSize), (int)(y / brushSize));
                texture.SetPixel(currentTextureX, currentTextureY, brushColor);
            }
        }

        texture.Apply();
    }

    private void OnSceneLoaded()
    {
        // Find the GameObject called "Right Hand" in the scene
        GameObject rightHand = GameObject.Find("Right Hand");

        if (rightHand != null)
        {
            // Assign it to the 'fromObject' field
            fromObject = rightHand;
        }
        else
        {
            Debug.Log("Right Hand not found in the scene.");
        }
    }
}
