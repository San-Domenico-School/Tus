using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/****************************************************************
 * Attached to: Camera Offset which is a component of the XRRig
 * Purpose: Turns the Camera Offset to match the direction y-axix 
 *          the Oculus headset rotation
 * Author: Reece
 * Version: 1.0
 ****************************************************************/

public class PlayerTurnController : MonoBehaviour
{
    private TusInputAction controls;
    private Rigidbody rb;

    private Quaternion turnInput = Quaternion.identity;

    // Use this for initialization
    void Awake()
    {
        controls = new TusInputAction();
        rb = GetComponentInParent<Rigidbody>();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Headset.HeadsetRotation.performed += ctx => PlayerTurn(ctx.ReadValue<Quaternion>());

        // reece: make input action then call playerturn with input vector3
    }

    void OnDisable()
    {
        controls.Disable();
    }

    //set camera rotation to vector (no tolerance, yet?)
    void PlayerTurn(Quaternion turn)
    {
        // Extract the Y-axis rotation from the headset rotation
        float headsetYRotation = turn.eulerAngles.y;

        // Get the current rotation of the Camera Offset object
        Vector3 currentRotation = transform.eulerAngles;

        // Update the Y-axis rotation to match the headset's Y-axis rotation
        transform.rotation = Quaternion.Euler(currentRotation.x, headsetYRotation, currentRotation.z);
    }
}