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
    [SerializeField] GameObject RedButton;

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
        Debug.Log("outside " +PaintManager);

        if (other.gameObject.CompareTag("RedButton"))
        {
            // TODO make these lines of code not throw an error 
            other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            PaletteController.Instance.addRed();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
            //PaintManager.GetComponent<Painter>().SetPaintColor(new Color(1, 1, 1, 1));

        }

        if (other.gameObject.CompareTag("BlueButton"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            PaletteController.Instance.addBlue();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }

        if (other.gameObject.CompareTag("YellowButton"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            PaletteController.Instance.addYellow();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }

        if (other.gameObject.CompareTag("ResetButton"))
        {
            PaletteController.Instance.resetColors();
            other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }
    }

}
