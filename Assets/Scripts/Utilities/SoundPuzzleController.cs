using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPuzzleController : MonoBehaviour
{
    public bool isMatched = false;

    [SerializeField] GameObject[] indicators;
    [SerializeField] GameObject[] selectors;

    private int wait = 0;
    private bool playindicators = true;
    private int lastplayedindex = 4;
    private int index = 0;
    

    void FixedUpdate()
    {
        if (wait > 0)
        {
            wait += 1;
            wait %= 30;
        }
        else
        {
            if (playindicators)
            {
                if (!indicators[index].GetComponent<ColorSoundIndicatorController>().getIsPlaying() && lastplayedindex != index)
                {
                    indicators[index].GetComponent<ColorSoundIndicatorController>().Play();
                    lastplayedindex = index;
                }
                else if (!indicators[index].GetComponent<ColorSoundIndicatorController>().getIsPlaying())
                {
                    index += 1;
                    if (index > 4)
                    {
                        index = 0;
                        playindicators = !playindicators;
                    }
                    wait = 1;
                }
            }
            else
            {
                if (!selectors[index].GetComponent<ColorSoundEntryController>().getIsPlaying() && lastplayedindex != index)
                {
                    selectors[index].GetComponent<ColorSoundEntryController>().Play();
                    lastplayedindex = index;
                }
                else if (!selectors[index].GetComponent<ColorSoundEntryController>().getIsPlaying())
                {
                    index += 1;
                    if (index > 4)
                    {
                        index = 0;
                        playindicators = !playindicators;
                    }
                    wait = 1;
                }
            }
        }
        isMatched = true;
        for (int i = 0; i < 5; ++i)
        {
            if (!selectors[i].GetComponent<ColorSoundEntryController>().ColorIsCorrect())
            {
                isMatched = false;
            }
        }
    }
}
