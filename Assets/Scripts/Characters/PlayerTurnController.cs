using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/**************************************************
 * Attached to: 
 * Purpose:
 * Author:
 * Version:
 *************************************************/

public class PlayerTurnController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    bool isLeftHanded;

    private TusInputAction controls;
    private Rigidbody rb;
    float distToGround;
    
    private Vector3 turnInput = Vector3.zero;
    private Vector2 moveInput = Vector2.zero;
    private bool jumpInput = false;
    
    // Use this for initialization
    void Awake()
    {
        controls = new TusInputAction();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void OnEnable()
    {
        controls.Enable();
        if (!isLeftHanded)
        {
            controls.PlayerControl_RightHanded.Move.performed += ctx => PlayerMove(ctx.ReadValue<Vector2>());
            controls.PlayerControl_RightHanded.Move.canceled += ctx => moveInput = Vector2.zero;
            controls.PlayerControl_RightHanded.Jump.performed += ctx => PlayerJump();
        }
        else
        {
            controls.PlayerControl_LeftHanded.Move.performed += ctx => PlayerMove(ctx.ReadValue<Vector2>());
            controls.PlayerControl_LeftHanded.Move.canceled += ctx => moveInput = Vector2.zero;
            controls.PlayerControl_LeftHanded.Jump.performed += ctx => PlayerJump();

        }
    }

    void OnDisable()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);

            Vector3 targetVelocity = moveVector * moveSpeed * moveVector.magnitude;

            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        }
    }

    //set movement vector if move within tolerance
    void PlayerMove(Vector2 move)
    {
        Debug.Log("called");
        if (move.magnitude > 0.05)
        {
            moveInput = move;
        }
        else
        {
            moveInput = Vector2.zero;
        }
    }

    //set camera rotation to vector
    void PlayerTurn(Vector3 turnInput)
    {
        
    }

    //if touching ground, set jumpinput to true
    void PlayerJump()
    {
        
    }

    bool IsGrounded()
    {
        return Physics.Raycast(GetComponent<Transform>().position, -Vector3.up, distToGround + 0.1f);
    }
}
