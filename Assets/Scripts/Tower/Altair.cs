using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Tuan Le
//1/28/2025
//Altar interact with player
public class Altair : MonoBehaviour
{
    public bool inProx = false;
    [SerializeField] GameObject switchObject;

    [SerializeField] GameObject barrier;
    private float startingTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //what happen after player interact with the pedestal
        if(inProx)
        {
            switchObject.SetActive(true);
            ManageBarrier();
            //gameObject.SetActive(false);
        }
    }

    //if player touch the altar then the paint brush is placed on it
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Right Hand")
        {
            if(inProx == false)
            {
                startingTime = Time.time;
            }
            inProx = true;
            Debug.Log("ouch");
        }
    }

    //trigger and close the barrier
    private void ManageBarrier()
    {
        if((Time.time-startingTime) > 50)
        {
            barrier.SetActive(false);
        }
        else
        {
            barrier.SetActive(true);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.tag == "Player")
    //     {
    //         inProx = false;
    //         Debug.Log("ouch");
    //     }
    // }
}
