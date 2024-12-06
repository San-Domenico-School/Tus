using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/**************************************************
 * Attached to: 
 * Purpose: Turn "Head" sphere according to headset rotation
 * Author: Reece
 * Version: 1.0
 *************************************************/

public class PlayerTurnController : MonoBehaviour
{
    [SerializeField] float turnSpeed;

    private TusInputAction controls;
    private Rigidbody rb;
    
    private Vector3 turnInput = Vector3.zero;
    
    // Use this for initialization
    void Awake()
    {
        controls = new TusInputAction();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        controls.Enable();
        //controls.PlayerControl_RightHanded.Move.performed += ctx => PlayerMove(ctx.ReadValue<Vector2>());
        //controls.PlayerControl_RightHanded.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Headset.HeadsetRotation.performed += ctx => PlayerTurn(ctx.ReadValue<Vector3>());

        // reece: make input action then call playerturn with input vector3
    }

    void OnDisable()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 turnVector = new Vector3(0, turnInput.y, 0);

        Quaternion rotationQuart = Quaternion.Euler(turnVector.x, turnVector.y, turnVector.z); // 0, y, 0

        rb.rotation = rotationQuart;
    }

    //set camera rotation to vector (no tolerance, yet?)
    void PlayerTurn(Vector3 turn)
    {
        Debug.Log("headset turn called");

        turnInput = turn;
    }
}
