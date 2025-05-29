using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**************************************************
 * Attached to: paint brush
 * Purpose: switch the color of the paint and the paintbrush;
 * also turns off paintbrush model when game is over.
 * Author: Logan and teddy
 * Version: 1.0
 *************************************************/
public class PaletteInteractor : MonoBehaviour
{
    private Painter paintManager;

    public bool GameIsOver = false;
    [SerializeField] private GameObject paintbrushmodel;

    private void Update()
    {
        if (GameIsOver == true)
        {
            paintbrushmodel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        paintManager = GameObject.FindObjectOfType<Painter>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("RedButton"))
        {
            // TODO make these lines of code not throw an error 
            PaletteController.Instance.addRed();
            paintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
            //PaintManager.GetComponent<Painter>().SetPaintColor(new Color(1, 1, 1, 1));

        }

        if (other.gameObject.CompareTag("BlueButton"))
        {
            PaletteController.Instance.addBlue();
            paintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }

        if (other.gameObject.CompareTag("YellowButton"))
        {
            PaletteController.Instance.addYellow();
            paintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }

        if (other.gameObject.CompareTag("ResetButton"))
        {
            PaletteController.Instance.resetColors();
            paintManager.GetComponent<Painter>().SetPaintColor(PaletteController.Instance.GetCurrentColor());
        }
    }
}
