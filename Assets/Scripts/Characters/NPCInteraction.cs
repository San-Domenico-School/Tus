using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void ActivateTextBox(bool activate)
    {
        dialogue.SetActive(activate);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("NPCInteracter_24: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("NPC(Painted)"))
        {
            ActivateTextBox(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("NPCInteracter_32: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateTextBox(false);
        }
    }
}


