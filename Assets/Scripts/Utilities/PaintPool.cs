using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * Attached to: paint pools
 * Purpose: add more paint to paintRemaining in the PaintbrushController
 * Author: Seamus 
 * Version: 1.0
 *************************************************/

public class PaintPool : MonoBehaviour
{
    [SerializeField] GameObject paintManager;
    
    
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (paintManager.GetComponent<PaintbrushController>().paintRemaining < 100f)
            {
                paintManager.GetComponent<PaintbrushController>().paintRemaining += 20 * Time.deltaTime;
            }
        }
        Debug.Log(paintManager.GetComponent<PaintbrushController>().paintRemaining);


    }
}
