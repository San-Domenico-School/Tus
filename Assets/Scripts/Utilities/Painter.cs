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
    [SerializeField] float brushSize = .5f;
    [SerializeField] public Color paintColor = Color.white;
    [SerializeField] float rayMaxDistance = 30f;

    [SerializeField] private Material paintBlitMaterial;

    //public float paintRemaining { get; set; } = 50;
    public float paintRemaining = 500;


    private void Awake()
    {
        paintAction = new TusInputAction();

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
        if (hit.transform == null || !ObjectStatisticsUtility.IsPaintable(hit.transform.gameObject))
            return;

        PaintTexture(hit.textureCoord, hit.transform.GetComponent<PaintableObject>());
    }

    //paints the texture at the UV cordate with diameter of the brushSize and shape of brush 
    private void PaintTexture(Vector2 uv, PaintableObject paintable)
    {
        RenderTexture rt = paintable.paintRT;

        paintBlitMaterial.SetVector("_PaintUV", new Vector4(uv.x, uv.y, 0, 0));
        paintBlitMaterial.SetFloat("_Radius", 0.03f);
        paintBlitMaterial.SetColor("_PaintColor", paintColor);

        RenderTexture temp = RenderTexture.GetTemporary(rt.width, rt.height);
        Graphics.Blit(rt, temp);
        Graphics.Blit(temp, rt, paintBlitMaterial);
        RenderTexture.ReleaseTemporary(temp);
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
