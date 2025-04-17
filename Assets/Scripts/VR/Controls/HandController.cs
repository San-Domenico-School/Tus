using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionReference gripInput;
    [SerializeField] private InputActionReference triggerInput;
    [SerializeField] private InputActionReference indexInput;    // this is for the index finger
    [SerializeField] private InputActionReference thumbInput;

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input actions if the object has an animator
        if (animator)
        {
            GetInputActions();
        }
        
    }

    private void GetInputActions()
    {
        // Set up actions in Input System
        float grip = gripInput.action.ReadValue<float>();
        float trigger = triggerInput.action.ReadValue<float>();

        animator.SetFloat("Grip", grip);
        animator.SetFloat("Trigger", trigger);

        // Set up actions in Input System if they exist
        if (indexInput != null)
        {
            float indexTouch = indexInput.action.ReadValue<float>();
            animator.SetFloat("Index", indexTouch);
        }

        if (thumbInput != null)
        {
            float thumbTouch = thumbInput.action.ReadValue<float>();
            animator.SetFloat("Thumb", thumbTouch);
        }
    }
}
