using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionDetect : MonoBehaviour
{
    [SerializeField] private PaintbrushControllerTeddy paintManager;

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the object we're colliding with has the tag "Pool"
        if (collision.gameObject.CompareTag("Pool"))
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