using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteController : MonoBehaviour
{
    private GameObject selectedColorDisplay;

    // used for weighted average
    private int mixedColors = 0;

    // #'s of button presses
    private int reds = 0;
    private int yellows = 0;
    private int blues = 0;

    // current color on palette
    private Color currentColor;
    

    // Start is called before the first frame update
    void Start()
    {
        selectedColorDisplay = transform.Find("SelectedColor").gameObject;

        currentColor = new Color(0, 0, 0);
    }

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

    public void resetColors()
    {
        mixedColors = 0;
        reds = 0;
        yellows = 0;
        blues = 0;

        currentColor = new Color(0, 0, 0);
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    private void computeColor()
    {
        float red = (float)reds / mixedColors;
        float yellow = (float)yellows / mixedColors;
        float blue = (float)blues / mixedColors;

        currentColor = ConvertRYBtoUnityColor(red, yellow, blue);

        selectedColorDisplay.GetComponent<MeshRenderer>().material.color = currentColor;
    }

    private static Color ConvertRYBtoUnityColor(float r, float y, float b)
    {
        // clamp values to range of 0-1
        r = Mathf.Clamp01(r);
        y = Mathf.Clamp01(y);
        b = Mathf.Clamp01(b);

        // convert from ryb to rgb
        float r1 = r + y - Mathf.Min(r, y);
        float g1 = y + b - Mathf.Min(y, b);
        float b1 = b + r - Mathf.Min(b, r);

        return new Color(r1, g1, b1);
    }
}
