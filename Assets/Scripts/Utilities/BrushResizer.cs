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
        if (other.gameObject.CompareTag("BrushSizeUp"))
        {
            PaintManager.GetComponent<PaintSizer>().SetBrushSize(PaintSizer.Instance.increaseSize());
        }

        if (other.gameObject.CompareTag("BrushSizeDown"))
        {
            PaintManager.GetComponent<PaintSizer>().SetBrushSize(PaintSizer.Instance.decreaseSize());
        }
    }
}
