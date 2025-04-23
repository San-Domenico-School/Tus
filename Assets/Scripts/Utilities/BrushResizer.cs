using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrushResizer : MonoBehaviour
{
    [SerializeField] GameObject PaintManager, BrushUp, BrushDown;

    // Start is called before the first frame update
    void OnSceneLoaded()
    {
        PaintManager = GameObject.Find("PaintManager");
    }


    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("BrushSizeUp"))
        {
            Painter.brushSize += 0.002f;
            
        }

        if (other.gameObject.CompareTag("BrushSizeDown"))
        {
            Painter.brushSize -= 0.002f;
            
        }
    }
}
