using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaintbrushController : MonoBehaviour
{
    private TusInputAction paintAction;


    [SerializeField] GameObject fromObject; 
    [SerializeField] Texture2D brush;
    [SerializeField] float brushSize = .5f;
    [SerializeField] float targetTexelDensity = 20f;
    [SerializeField] Color paintColor = Color.white;
    [SerializeField] float rayMaxDistance = 30f;
    public float paintRemaining = 50f;


    private void Awake() 
    {
        paintAction = new TusInputAction();
    }

    private void OnEnable() 
    {
        paintAction.Enable();
        paintAction.DominantArm_RightHanded.Paint.performed += ctx => HandlePainting();
    }

    private void HandlePainting()
    {
        if(paintRemaining >= 0)
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
    
    private void PaintObject()
    {
        RaycastHit hit;
        Physics.Raycast(fromObject.transform.position, fromObject.transform.forward, out hit, rayMaxDistance);
        
        
        if(hit.transform == null)
            return;
        
        Texture2D texture = ObjectStatisticsUtility.GetOrCreateObjectsTexture(hit.transform.gameObject,  targetTexelDensity);

        PaintTexture(hit.textureCoord, texture);
    }


    //paints the texture at the UV cordate with diameter of the brushSize and shape of brush 
    private void PaintTexture(Vector2 uv, Texture2D texture)
    {
        uv.x *= texture.width;
        uv.y *= texture.height; 

        int brushWidth = (int)(brush.width * brushSize);
        int brushHeight = (int)(brush.height * brushSize);

        //paints the paintColor where the r value of the brush is 255
        for(int x = 0; x < brushWidth; x++)
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

}
