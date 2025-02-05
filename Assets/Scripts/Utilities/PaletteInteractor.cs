using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * Attached to: paint brush
 * Purpose: switch the color of the paint and the paintbrush
 * Author: Logan 
 * Version: 1.0
 *************************************************/
public class PaletteInteractor : MonoBehaviour
{
    [SerializeField] GameObject PaintManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedButton"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            PaletteController.Instance.addRed();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());

        }

        if (other.gameObject.CompareTag("BlueButton"))
        {
            PaletteController.Instance.addBlue();
        }

        if (other.gameObject.CompareTag("YellowButton"))
        {
            PaletteController.Instance.addYellow();
        }

        if (other.gameObject.CompareTag("ResetButton"))
        {
            PaletteController.Instance.resetColors();
        }
    }

}
