using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSoundIndicatorController : MonoBehaviour
{
    [SerializeField] Color indicatorColor;

    AudioSource audioPlayer;
    MeshRenderer audioIndicator;

    private float delay = 1.0f;

    private bool isPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject colorIndicator = transform.Find("Indicator_Color").gameObject;
        colorIndicator.GetComponent<MeshRenderer>().material.color = indicatorColor;

        audioIndicator = transform.Find("Indicator_Light").gameObject.GetComponent<MeshRenderer>();
        audioPlayer = transform.Find("Indicator_Light").gameObject.GetComponent<AudioSource>();
    }

    // Update is called 50 times per second
    void FixedUpdate()
    {
        updateCounter += 1;
        seconds += updateCounter / 50;
        updateCounter %= 50;
    }

    void Play()
    {
        isPlaying = true;
        audioPlayer.Play();
        StartCoroutine(checkAudioPlaybackFinished());
    }

    private System.Collections.IEnumerator checkAudioPlaybackFinished()
    {
        while(audioPlayer.isPlaying)
        {
            yield return null;
        }

        yield return WaitForSeconds(delay);

        isPlaying = false;
    }

    bool getIsPlaying()
    {
        return isPlaying;
    }
}
