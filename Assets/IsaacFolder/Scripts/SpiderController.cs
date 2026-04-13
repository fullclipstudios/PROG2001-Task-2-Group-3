using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SpiderController : MonoBehaviour
{
    // Movement & Reset
    public float speed = 15f;
    public float jumpForce = 5f;
    public float keyRotationSpeed = 150f;
    public float mouseRotationSpeed = 1f;
    public Transform spider;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private float rotateKeys;
    private float rotateMouse;

    // Collectibles & UI
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject doorTrigger;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        defaultPosition = spider.position;
        defaultRotation = spider.rotation;

        SetCountText();
        winTextObject.SetActive(false);
    }

    // Called by Player Input (Send Messages)
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnRotateKeys(InputValue value)
    {
        rotateKeys = value.Get<float>(); // -1 to 1
    }

    void OnRotateMouse(InputValue value)
    {
        rotateMouse = value.Get<float>(); // mouse delta
    }

    void OnJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Reset() // stop movement?
    {
        spider.position = defaultPosition;
        spider.rotation = defaultRotation;
    }

    void FixedUpdate()
    {
        // Movement direction based on input
        Vector3 movement = transform.forward * movementZ + transform.right * movementX;

        if (movement.magnitude > 0.01f) // 
        {
            rb.velocity = new Vector3(
                movement.normalized.x * speed,
                rb.velocity.y, // preserve gravity
                movement.normalized.z * speed
            );
        }
        else
        {
            rb.velocity = new Vector3(
                0f,
                rb.velocity.y, // preserve gravity
                0f
            );
        }

        float keyRotation = rotateKeys * keyRotationSpeed * Time.fixedDeltaTime;
        float mouseRotation = rotateMouse * mouseRotationSpeed;

        // Rotation
        transform.Rotate(Vector3.up * (keyRotation + mouseRotation));

        float currentSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude;

        if (currentSpeed < 0.05f)
            currentSpeed = 0f;

        animator.SetFloat("Speed", currentSpeed);
    }

    void SetCountText()
    {
        countText.text = "Count - " + count.ToString();
        if(count >= 1)
        {
            doorTrigger.gameObject.SetActive(false);
        }

        if(count >= 4) // CHANGE COUNT TO SUIT
        {
            winTextObject.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}