using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/**************************************************
 * Attached to: Character
 * Purpose: Move character based on joystick input
 * Author: Nathaniel de Marcellus
 * Version: 1.0
 *************************************************/

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    bool isLeftHanded;

    private TusInputAction controls;
    private Rigidbody rb;
    float distToGround;
    
    private Vector2 moveInput = Vector2.zero;
    private bool isInBoundary = false;
    
    // Use this for initialization
    void Awake()
    {
        controls = new TusInputAction();

        rb = GetComponentInParent<Rigidbody>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InnerBorder"))
        {
            isInBoundary = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InnerBorder"))
        {
            isInBoundary = false;
        }
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
        if (isInBoundary)
        {
            moveInput /= 3;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(GetComponent<Transform>().position, -Vector3.up, distToGround + 0.1f);
    }



}
