using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 7f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    // To prevent duplicate collision handling
    private HashSet<GameObject> processedCubes = new HashSet<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the player from rotating
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Prevent missed collisions
    }

    void Update()
    {
        HandleMovement();
        HandleJump();

        // Check if the player has fallen
        if (transform.position.y <= -2)
        {
            GameManager.instance.GameOver();
        }
    }

    private void HandleMovement()
    {
        // Left and right input only
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Constant forward movement
        Vector3 moveDirection = transform.forward + transform.right * moveHorizontal;

        // Set the speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        // Apply movement
        Vector3 desiredVelocity = new Vector3(moveDirection.x * speed, rb.velocity.y, transform.forward.z * speed);
        rb.velocity = desiredVelocity;
    }

    private void HandleJump()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize ground check sphere in the Scene view
        if (groundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has hit a cube
        if (collision.gameObject.CompareTag("Cube"))
        {
            // Prevent duplicate handling for the same cube
            if (processedCubes.Contains(collision.gameObject))
                return;

            processedCubes.Add(collision.gameObject);

            Debug.Log($"Collision detected with cube: {collision.gameObject.name}");

            // Find the RunnerController component
            RunnerSpawnerController runnerController = FindObjectOfType<RunnerSpawnerController>();
            if (runnerController != null)
            {
                // Get the OperationsController component from the cube
                OperationsController operationsController = collision.gameObject.GetComponent<OperationsController>();
                if (operationsController != null)
                {
                    var operation = operationsController.operation;
                    runnerController.DuplicateSphere(operation);
                }
            }

            // Destroy the cube after a small delay to ensure collision completes
            Destroy(collision.gameObject, 0.1f);
        }
    }
}
