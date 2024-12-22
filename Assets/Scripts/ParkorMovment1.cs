using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMovement : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 10f;
    public float runSpeed = 15f;
    public float jumpForce = 7f;
    public float gravity = -9.81f;
    public float climbSpeed = 5f;

    // Ground and climbing checks
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform climbCheck;
    public float climbDistance = 0.5f;
    public LayerMask climbableMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isClimbing;

    // Animator reference for child
    public Animator animator;

   void Start()
{
    controller = GetComponent<CharacterController>();

    // Find the Animator in the child object
    animator = GetComponentInChildren<Animator>();

    // Check if Animator is assigned
    if (animator == null)
    {
        Debug.LogError("Animator component is missing! Make sure the child has an Animator component.");
    }
}


    void Update()
    {
        HandleMovement();
        HandleClimbing();
        UpdateAnimator();
    }

    void HandleMovement()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Check for running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity
        if (!isClimbing)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        // Set movement-related parameters for animations
        animator.SetBool("isRunning", isRunning && (x != 0 || z != 0));
        animator.SetBool("isWalking", !isRunning && (x != 0 || z != 0));
        animator.SetBool("isIdle", x == 0 && z == 0 && isGrounded);
    }

    void HandleClimbing()
    {
        // Check for climbable objects
        isClimbing = Physics.CheckSphere(climbCheck.position, climbDistance, climbableMask);

        if (isClimbing)
        {
            // Disable gravity and move upwards
            velocity.y = 0;
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 climbDirection = transform.up * verticalInput;
            controller.Move(climbDirection * climbSpeed * Time.deltaTime);
        }

        // Set climbing parameter for animations
        animator.SetBool("isClimbing", isClimbing);
    }

   void UpdateAnimator()
{
    // Check if the character is moving
    bool isWalking = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);

    // Update animator parameters
    animator.SetBool("isWalking", isWalking && isGrounded); // Walking only when grounded
    animator.SetBool("isJumping", !isGrounded && !isClimbing); // Jumping if not grounded
    animator.SetBool("isClimbing", isClimbing); // Climbing animation
    animator.SetBool("isIdle", !isWalking && isGrounded); // Idle when not moving and grounded
}

    void OnDrawGizmos()
    {
        // Visualize ground and climb checks
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }

        if (climbCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(climbCheck.position, climbDistance);
        }
    }
}