using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSoundIndicatorController : MonoBehaviour
{
    [SerializeField] Color indicatorColor;

    AudioSource audioPlayer;
    MeshRenderer audioIndicator;

    int updateCounter = 0;
    int seconds = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameObject colorIndicator = transform.Find("Indicator_Color").gameObject;
        colorIndicator.GetComponent<MeshRenderer>().material.color = indicatorColor;

        GameObject soundIndicator = transform.Find("Indicator_Light").gameObject;
        audioIndicator.GetComponent<MeshRenderer>();
        audioPlayer.GetComponent<AudioSource>();
    }

    // Update is called 50 times per second
    void FixedUpdate()
    {
        updateCounter += 1;
        seconds += updateCounter / 50;
        updateCounter %= 50;
    }
}
