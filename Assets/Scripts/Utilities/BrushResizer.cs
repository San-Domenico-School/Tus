using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushResizer : MonoBehaviour
{
    [SerializeField] GameObject PaintManager;
    private TusInputAction controls;
    bool isLeftHanded;
    void Awake()
    {
        controls = new TusInputAction();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        controls.Enable();
        if (!isLeftHanded)
        {
            controls.DominantArm_RightHanded.BrushSizeUp.performed += ctx => Painter.brushSize += 0.200f;
            controls.DominantArm_RightHanded.BrushSizeDown.performed += ctx => Painter.brushSize -= 0.200f;
        }
            
            
    }

    void OnDisable()
    {
        controls.Disable();
    }


    //private void OnTriggerStay(Collider other)
    //{
        
       // if (other.gameObject.CompareTag("BrushSizeUp"))
       // {
           // Painter.brushSize += 0.002f;
            
       // }

      //  if (other.gameObject.CompareTag("BrushSizeDown"))
       // {
         //   Painter.brushSize -= 0.002f;
            
       // }
    //}
}
