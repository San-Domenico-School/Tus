using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**************************************************
 * Attached to: paint manager 
 * Purpose: paint
 * Author: Seamus/Teddy/Nat
 * Version: 2.0
 *************************************************/

public class Painter : MonoBehaviour
{
    //public static Painter Instance;
    public float paintRemaining = 500;
    public Color paintColor = Color.white;
    [SerializeField] public float brushSize = 1f;
    [SerializeField] float rayMaxDistance = 30f;
    [SerializeField] private Material paintBlitMaterial;

    
    private GameObject fromObject;
    private TusInputAction paintAction;
    private bool isPainting;
    private bool brushSizeUp;
    private bool brushSizeDown;


    public Color GetPaintColor()
    {
        return paintColor;
    }
    public void SetPaintColor(Color color)
    {
        paintColor = color;
    }

    private void Awake()
    {
        paintAction = new TusInputAction();
    }

    private void OnEnable()
    {
        paintAction.Enable();
        paintAction.DominantArm_RightHanded.Paint.performed += ctx => isPainting = true; 
        paintAction.DominantArm_RightHanded.Paint.canceled += ctx => isPainting = false;
        
        paintAction.DominantArm_RightHanded.BrushSizeUp.performed += ctx => brushSizeUp = true;
        paintAction.DominantArm_RightHanded.BrushSizeUp.canceled += ctx => brushSizeUp = false;

        paintAction.DominantArm_RightHanded.BrushSizeDown.performed += ctx => brushSizeDown = true;
        paintAction.DominantArm_RightHanded.BrushSizeDown.canceled += ctx => brushSizeDown = false;


        fromObject = GameObject.Find("Right hand");
    }

    private void Update()
    {
        if (isPainting && paintRemaining > 0)
        {
            PaintObject();
            paintRemaining -= Time.deltaTime; // every second lose one paint unit
        }
        
        BrushSizing();
    }

    private void PaintObject()
    {
        // Shoots ray for fromObject forward
        RaycastHit hit;
        Physics.Raycast(fromObject.transform.position, fromObject.transform.forward, out hit, rayMaxDistance); 

        // Checks if what it hit exist and is paintable
        if (hit.transform == null || !ObjectStatisticsUtility.IsPaintable(hit.transform.gameObject))
            return;

        PaintTexture(hit.textureCoord, hit.transform.GetComponent<PaintableObject>());
    }

    //paints the texture at the UV cordate with diameter of the brushSize and shape of brush 
    private void PaintTexture(Vector2 uv, PaintableObject paintable)
    {
        RenderTexture renderTexture = paintable.paintRT;

        paintBlitMaterial.SetVector("_PaintUV", new Vector2(uv.x, uv.y));
        paintBlitMaterial.SetFloat("_Radius", brushSize);
        paintBlitMaterial.SetColor("_PaintColor", paintColor);
        paintBlitMaterial.SetFloat("_ObjectArea", Mathf.Sqrt(paintable.fullTextureArea));

        RenderTexture temp = RenderTexture.GetTemporary(renderTexture.width, renderTexture.height);
        Graphics.Blit(renderTexture, temp);
        Graphics.Blit(temp, renderTexture, paintBlitMaterial);
        RenderTexture.ReleaseTemporary(temp);
    }

    private void BrushSizing()
    {
        if (brushSizeUp && brushSize < 2.5)
        {
            brushSize += 0.02f;
        }
        if (brushSizeDown && brushSize > 0.5f)
        {
            brushSize -= 0.02f;
        }
    }

    // does this do anything
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
