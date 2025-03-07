using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSoundEntryController : MonoBehaviour
{
    [SerializeField] Material TargetMat;
    Color TargetColor;

    AudioSource audioPlayer;
    MeshRenderer audioIndicator;

    private float delay = 1.0f;

    private bool isPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        this.TargetColor = TargetMat.color;

        GameObject colorIndicator = transform.Find("Indicator_Color").gameObject;
        colorIndicator.GetComponent<MeshRenderer>().material.color = Color.white;

        audioIndicator = transform.Find("Indicator_Light").gameObject.GetComponent<MeshRenderer>();
        audioIndicator.material.color = Color.grey;
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void Play()
    {
        isPlaying = true;
        if (!ColorIsCorrect())
        {
            audioIndicator.material.color = Color.green;
        }
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
        if (!ColorIsCorrect())
        {
            audioIndicator.material.color = Color.grey;
        }
    }

    public bool getIsPlaying()
    {
        return isPlaying;
    }

    public bool ColorIsCorrect()
    {
        GameObject colorIndicator = transform.Find("Indicator_Color").gameObject;
        Color indicatorColor = colorIndicator.GetComponent<MeshRenderer>().material.color;
        float r = Mathf.Abs(TargetColor.r - indicatorColor.r);
        float g = Mathf.Abs(TargetColor.g - indicatorColor.g);
        float b = Mathf.Abs(TargetColor.b - indicatorColor.b);

        // being little more forgiving with color here - if two colors are more accurate one can be further off
        if (r + g + b < 0.3)
        {
            audioIndicator.material.color = Color.blue;
            return true;
        }
        return false;
    }
}
