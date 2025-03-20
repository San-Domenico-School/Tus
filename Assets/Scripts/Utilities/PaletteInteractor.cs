using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**************************************************
 * Attached to: paint brush
 * Purpose: switch the color of the paint and the paintbrush
 * Author: Logan 
 * Version: 1.0
 *************************************************/
public class PaletteInteractor : MonoBehaviour
{
    public Painter PaintManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded()
    {
        PaintManager = GameObject.FindObjectOfType<Painter>();

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("RedButton"))
        {
            // TODO make these lines of code not throw an error 
            PaletteController.Instance.addRed();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
            //PaintManager.GetComponent<Painter>().SetPaintColor(new Color(1, 1, 1, 1));

        }

        if (other.gameObject.CompareTag("BlueButton"))
        {
            PaletteController.Instance.addBlue();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }

        if (other.gameObject.CompareTag("YellowButton"))
        {
            PaletteController.Instance.addYellow();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }

        if (other.gameObject.CompareTag("ResetButton"))
        {
            PaletteController.Instance.resetColors();
            PaintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }
    }

}
