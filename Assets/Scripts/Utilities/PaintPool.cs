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
    [SerializeField] float MaxPaintingSeconds = 60;
    

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (paintManager.GetComponent<Painter>().paintRemaining < MaxPaintingSeconds)
            {
                paintManager.GetComponent<Painter>().paintRemaining += Time.deltaTime * 15;
            }

            Debug.Log(paintManager.GetComponent<Painter>().paintRemaining);
        }
    }
    
}
