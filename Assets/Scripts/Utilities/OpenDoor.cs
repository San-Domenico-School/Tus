using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject puzzle;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (puzzle.GetComponent<SoundPuzzleController>().isMatched)
        {
            animator.SetBool("isOpen", true);
        }
    }
}
