using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterEnabler : MonoBehaviour
{
    [SerializeField] GameObject painterObject;

    // Start is called before the first frame update
    void Start()
    {
        painterObject.GetComponent<Painter>().PainterActive = false;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            painterObject.GetComponent<Painter>().PainterActive = true;
        }
    }
}
