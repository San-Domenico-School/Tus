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
        return ++brushSize;
    }

    public float decreaseSize()
    {
        if (brushSize >= 1)
        {
            brushSize--;
        }

        return brushSize;
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

        texture.Apply();
    }
}
