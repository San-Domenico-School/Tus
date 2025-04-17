using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushResizer : MonoBehaviour
{
    [SerializeField] GameObject PaintManager, BrushUp, BrushDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        PaintSizer.brushSize = new float();
        if (other.gameObject.CompareTag("BrushSizeUp"))
        {

            PaintSizer.Instance.increaseSize();
            PaintManager.GetComponent<PaintSizer>().SetBrushSize(PaintSizer.Instance.GetCurrentSize());
        }

        if (other.gameObject.CompareTag("BrushSizeDown"))
        {
            PaintSizer.Instance.decreaseSize();
            PaintManager.GetComponent<PaintSizer>().SetBrushSize(PaintSizer.Instance.GetCurrentSize());
        }
    }
}
   