using UnityEngine;
using UnityEngine.UI; 

public class SimpleFPSController : MonoBehaviour
{
    [Header("Speed")]

    [Tooltip("Speed of the player")]
    [SerializeField] private float speed = 5f;
    
    [Header("Jump & Gravity")]
    [Tooltip("Player gravity")]
    [SerializeField] private float gravity = -9.81f;
    [Tooltip("Jump Height")]
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Sensitivity")]
    [Tooltip("Mouse sens")]
    [SerializeField] private float mouseSensitivity = 100f;

    [SerializeField] private int count = 0;

    [SerializeField] private Text countText;
    [SerializeField] private GameObject winTextObject;

    

    [Header("Look clamping")]
    [Tooltip("Max down look")]
    [SerializeField] private float minLookAngle = -90f;
    [Tooltip("Max up Look")]
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
        countText.text = "Count - " + count.ToString();

        if(count >= 5) // CHANGE COUNT TO SUIT
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