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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag("RedButton"))
        {
            
        }
    }
}
