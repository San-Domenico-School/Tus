using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public Transform teleportDestination;
    bool isReady = true;

    IEnumerator IsReadySet(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isReady = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReady)
        {
            isReady = false;
            other.transform.position = teleportDestination.position;
            StartCoroutine(IsReadySet(3f));
        }
    }
}
