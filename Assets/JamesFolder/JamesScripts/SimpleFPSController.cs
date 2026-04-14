using UnityEngine;
using UnityEngine.UI; 

public class SimpleFPSController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 5f;

    [Header("Jump & Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 100f;

    [SerializeField] private int count = 0;

    [SerializeField] private Text countText;
    [SerializeField] private GameObject winTextObject;

    [Header("Look clamping")]
    [SerializeField] private float minLookAngle = -90f;
    [SerializeField] private float maxLookAngle = 90f; 

    public Transform cameraTransform;

    private CharacterController controller;
    private float yVelocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    count = 0;
    SetCountText();
}

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * speed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    void SetCountText()
{
    if (countText != null)
    {
        countText.text = "Score Count: " + count.ToString();
    }

    if (count >= 5)
    {
        if (winTextObject != null)
        {
            winTextObject.SetActive(true);
        }
    }
}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }
}