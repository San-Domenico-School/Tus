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
        audioIndicator.material.color = Color.grey;
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    void Play()
    {
        isPlaying = true;
        audioIndicator.material.color = Color.green;
        audioPlayer.Play();
        StartCoroutine(checkAudioPlaybackFinished());
    }

    private System.Collections.IEnumerator checkAudioPlaybackFinished()
    {
        while(audioPlayer.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(delay);

        isPlaying = false;
        audioIndicator.material.color = Color.grey;
    }

    bool getIsPlaying()
    {
        return isPlaying;
    }
}
