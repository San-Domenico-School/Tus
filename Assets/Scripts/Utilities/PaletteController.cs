using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**************************************************
 * Attached to: Paint Palette
 * Purpose: Select a color based on player input
 * Author: Nathaniel de Marcellus
 * Version: 1.0
 *************************************************/

public class PaletteController : MonoBehaviour
{
    public static PaletteController Instance;

    private GameObject selectedColorDisplay;
    private GameObject redButton;
    private GameObject yellowButton;
    private GameObject blueButton;
    private GameObject resetButton;

    // used for weighted average
    private int mixedColors = 0;

    // #'s of button presses
    private int reds = 0;
    private int yellows = 0;
    private int blues = 0;

    // current color on palette
    private Color currentColor;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        selectedColorDisplay = transform.Find("SelectedColor").gameObject;
        redButton = transform.Find("Red").gameObject;
        yellowButton = transform.Find("Yellow").gameObject;
        blueButton = transform.Find("Blue").gameObject;
        resetButton = transform.Find("Reset").gameObject;

        currentColor = new Color(0, 0, 0);
        selectedColorDisplay.GetComponent<MeshRenderer>().material.color = currentColor;
    }

    // get player click input to control palette 
    // void Update()
    // {
    //     if (Mouse.current.leftButton.wasPressedThisFrame)
    //     {
    //         Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
    //         RaycastHit hit;
    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             if (hit.collider.gameObject == redButton)
    //             {
    //                 addRed();
    //             }
    //             if (hit.collider.gameObject == yellowButton)
    //             {
    //                 addYellow();
    //             }
    //             if (hit.collider.gameObject == blueButton)
    //             {
    //                 addBlue();
    //             }
    //             if (hit.collider.gameObject == resetButton)
    //             {
    //                 resetColors();
    //             }
    //         }
    //     }
    // }

    // mutators to increase paint levels
    public void addRed()
    {
        reds += 1;
        computeColor();
    }

    public void addYellow()
    {
        yellows += 1;
        computeColor();
    }

    public void addBlue()
    {
        blues += 1;
        computeColor();
    }

    //set color to black and reset all color values
    public void resetColors()
    {
        mixedColors = 0;
        reds = 0;
        yellows = 0;
        blues = 0;

        currentColor = new Color(0, 0, 0);

        selectedColorDisplay.GetComponent<MeshRenderer>().material.color = currentColor;
    }

    // setters and getters for current set color
    public void SetCurrentColor(Color color)
    {
        currentColor = color;
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    // calculate color based on a weighted average of ryb values
    private void computeColor()
    {
        mixedColors++;

        float red = (float)reds / mixedColors * 255.0f;
        float yellow = (float)yellows / mixedColors * 255.0f;
        float blue = (float)blues / mixedColors * 255.0f;

        // Debug.Log(red);
        // Debug.Log(yellow);
        // Debug.Log(blue);

        currentColor = ConvertRYBtoUnityColor(red, yellow, blue);

        selectedColorDisplay.GetComponent<MeshRenderer>().material.color = currentColor;
    }

    // convert from ryb to rgb, return unity color object
    private static Color ConvertRYBtoUnityColor(float r, float y, float b)
    {
        // Author: Arah J. Leonard, Translated from Python to C# by Nathaniel de Marcellus
        // Code licensed under LGPL - http://www.gnu.org/copyleft/lesser.html


        // remove whiteness from color
        float w = (float)Mathf.Min(r, y, b);
        r = (float)r - w;
        y = (float)y - w;
        b = (float)b - w;

        float my = Mathf.Max(r, y, b);

        // remove green from yellow & blue
        float g = Mathf.Min(y, b);
        y -= g;
        b -= g;

        if (b != 0 && g != 0) {
            b *= 2.0f;
            g *= 2.0f;
        }

        // redistrubute yellow remaining
        r += y;
        g += y;

        // normalize values
        float mg = Mathf.Max(r, g, b);
        if (mg != 0) {
            float n = my / mg;
            r *= n;
            g *= n;
            b *= n;
        }

        // add the white back into the color
        r += w;
        g += w;
        b += w;

        // normalize values to the range 0..1
        r /= 255.0f;
        g /= 255.0f;
        b /= 255.0f;

        // scale for color saturation
        float scale = 1.0f / Mathf.Max(r, g, b);

        return new Color(r * scale, g * scale, b * scale);
    }
}
