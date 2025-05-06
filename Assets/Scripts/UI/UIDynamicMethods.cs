using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * UIDynamicMethods.cs
 * 
 * Description:
 * This script routes PlayerInput actions to scene-specific GameObjects at runtime.
 * It is attached to the XR Rig to persist across scenes.
 * Instead of directly binding PlayerInput UnityEvents to scene objects (which may not
 * exist at load time), this script dynamically finds and forwards input events to the 
 * appropriate target object and method when they become available in the scene.
 * 
 * Use Case:
 * Useful when the PlayerInput component persists across scenes, but the action handlers
 * (such as UI controllers) exist only in specific scenes. Prevents broken UnityEvent bindings.
 * 
 * Author: Bruce Gustin
 * Date: May 5, 2025
 */

public class UIDynamicMethods : MonoBehaviour
{
    public void OnSimonPuzzleClick(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        GameObject simonPuzzle = GameObject.Find("SimonV2");
        if (simonPuzzle != null)
        {
            SimonPuzzle handler = simonPuzzle.GetComponent<SimonPuzzle>();
            if(handler != null)
            {
                handler.Interact();
            }
        }

    }

    // Additional methods can be added here to create new bindings for
    // all puzzles and other structures that are not present in the
    // opening scene.

}
