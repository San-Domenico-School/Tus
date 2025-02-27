using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionDetect : MonoBehaviour
{
    [SerializeField] private PaintbrushControllerTeddy paintManager;

    private void OnTriggerEnter()
    {
        Debug.Log("collision detected");
        if (gameObject.CompareTag("Pool"))
        {
            if (paintManager != null)
            {
               paintManager.GetComponent<PaintbrushControllerTeddy>().RefillPaint();
               Debug.Log("paint refill called");
            }
            else
            {
                Debug.LogError("PaintManager reference is not assigned in PoolCollisionTrigger!");
            }
        }
    }
}