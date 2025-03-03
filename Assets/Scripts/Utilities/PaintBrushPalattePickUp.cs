using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrushPalattePickUp : MonoBehaviour
{
    [SerializeField] private GameObject PalattePlaceHolder, PaintBrushPlaceHolder, LeftHand, RightHand, Palatte, PaintBrush;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (LeftHand && PalattePlaceHolder)
        {
            PalattePlaceHolder.SetActive(false) ;
            Palatte.SetActive(true);
        }

        if (RightHand && PaintBrushPlaceHolder)
        {
            PaintBrushPlaceHolder.SetActive(false);
            PaintBrush.SetActive(true);
        }
    }
}
