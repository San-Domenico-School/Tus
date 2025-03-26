using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**************************************************
 * Attached to: paint manager 
 * Purpose: shoot a ray out and paint the world at its location
 * Author: Seamus
 * Version: 1.1
 *************************************************/

public class PaintSizer : MonoBehaviour
{

    public static PaintSizer Instance;
    private TusInputAction paintAction;
    private bool isPainting;

    [SerializeField] GameObject fromObject;
    [SerializeField] Texture2D brush;
    [SerializeField] float brushSize = .5f;

    private float currentSize;

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
    }

    private void Update()
    {
    }

    public void SetCurrentSize(float size)
    {
        currentSize = size;
    }

    public float GetCurrentSize()
    {
        return currentSize;
    }

    public void SetBrushSize(float size)
    {
        brushSize = size;
    }

    public float increaseSize()
    {
        brushSize++;
        currentSize = brushSize;
        return currentSize;
    }

    public float decreaseSize()
    {
        if (brushSize >= 1)
        {
            brushSize--;
        }

         currentSize = brushSize;
        return currentSize;
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
                //brushColor = Color.Lerp(texture.GetPixel(currentTextureX, currentTextureY), paintColor, brushColor.r);
                //Debug.Log(texture.GetPixel(currentTextureX, currentTextureY));
                //brushColor = brush.GetPixel((int)(x / brushSize), (int)(y / brushSize));
                texture.SetPixel(currentTextureX, currentTextureY, brushColor);
            }
        }

        texture.Apply();
    }
}
