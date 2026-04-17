using UnityEngine;
using UnityEngine.UI; 
using TMPro;



public class SimpleFPSController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 5f;

    [Header("Jump & Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 100f;

    private int count = 0;


    [Header("Look clamping")]
    [SerializeField] private float minLookAngle = -90f;
    [SerializeField] private float maxLookAngle = 90f; 

    [Header("UI elements")]
    [SerializeField] private TMP_Text countText;
    [SerializeField] private GameObject winTextObject;
    [SerializeField] private int coinsToWin = 5;
    [SerializeField] private GameObject menuPanel;
    private bool isMenuOpen = false;

    [Header("Sound")]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip winSound;

    [Header("Camera")]
    public Transform cameraTransform;

    private CharacterController controller;
    private float yVelocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        

    count = 0;
    SetCountText();
    }

    void Update()
    {
        Look();
        Move();

        //if M is pressed, toggle the menu pop up
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }

   public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (menuPanel != null)
            menuPanel.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            // show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // pause game
            Time.timeScale = 0f;
        }
        else
        {
            // hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // resume game
            Time.timeScale = 1f;
        }
    }

    void Look()
    {

        //disable movement when menu open
        if (isMenuOpen) return;


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
        countText.text = "Coins: " + count.ToString() + "/" + coinsToWin;
    }

    //win condition
    if (count >= coinsToWin)
    {
        if (winTextObject != null)
        {
            winTextObject.SetActive(true);
        }
        //enable mouse for menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //stop player movement on the scene
        Time.timeScale = 0f;

        //victory sound fx
        AudioSource.PlayClipAtPoint(winSound, transform.position);

    }
}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            //pickup sound fx
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

            SetCountText();
        }
    }
}