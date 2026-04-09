using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Walking speed
    public float rotationSpeed = 150f; // Turning speed in degrees/sec
    public float jumpForce = 7f;       // Jump strength

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Optional: Freeze unwanted rotations (prevents tipping over)
        rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovementAndRotation();
    }

    void HandleMovementAndRotation()
    {
        // Get input
        float h = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        // Rotation
        if (Mathf.Abs(h) > 0.01f)
        {
            float yRotation = h * rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yRotation, 0f));
        }

        // Forward/back movement
        Vector3 move = transform.forward * v * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if player is on ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}