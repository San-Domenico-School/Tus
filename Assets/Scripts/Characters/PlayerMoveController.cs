using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/**************************************************
 * Attached to: 
 * Purpose:
 * Author:
 * Version:
 *************************************************/

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    bool isLeftHanded;

    private TusInputAction controls;
    private Rigidbody rb;
    float distToGround;
    
    private Vector2 moveInput = Vector2.zero;
    
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

        }
        else
        {
            controls.PlayerControl_LeftHanded.Move.performed += ctx => PlayerMove(ctx.ReadValue<Vector2>());
            controls.PlayerControl_LeftHanded.Move.canceled += ctx => moveInput = Vector2.zero;

        }
    }

    void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);
            Vector3 targetVelocity = moveVector * moveSpeed * moveVector.magnitude;

            rb.velocity = transform.TransformDirection(targetVelocity);
        }
    }

    // Set movement vector if move within tolerance
    void PlayerMove(Vector2 move)
    {
        if (move.magnitude > 0.05)
        {
            moveInput = move;
        }
        else
        {
            moveInput = Vector2.zero;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(GetComponent<Transform>().position, -Vector3.up, distToGround + 0.1f);
    }
}
