using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/**************************************************
 * Attached to: Camera Offset
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
        rb = GetComponentInParent<Rigidbody>();
        distToGround = GetComponentInParent<Collider>().bounds.extents.y;
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

    // Set movement vector if move within tolerance
    void PlayerMove(Vector2 move)
    {
        // Gives tolerance to movement
        if (move.magnitude > 0.05f)
        {
            moveInput = move;
        }
        else
        {
            moveInput = Vector2.zero;
        }

        if (IsGrounded())
        {
            // Use the camera's forward and right directions for movement
            Transform cameraTransform = Camera.main.transform; // Access the camera
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Project forward and right onto the horizontal plane (ignore Y-axis)
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Combine forward and right directions with move input
            Vector3 worldMoveDirection = (forward * moveInput.y + right * moveInput.x); // Swap x and y

            // Apply movement speed
            Vector3 targetVelocity = worldMoveDirection * moveSpeed;

            // Set the Rigidbody's velocity, preserving the current y velocity
            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(GetComponent<Transform>().position, -Vector3.up, distToGround + 0.1f);
    }
}
