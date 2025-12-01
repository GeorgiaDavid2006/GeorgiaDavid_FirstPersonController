using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonController : MonoBehaviour
{
    private CharacterController characterController;

    //Variables related to movement
    public float minSpeed = 0;
    public float speed;
    public float maxSpeed = 10;
    public float maxSprint = 20;
    public float acceleration = 5;
    public float deceleration = 5;
    Vector3 Direction;
    private float height = 2;
    private float crouchHeight = 1;
    private bool isCrouching;

    //Variables related to jumping
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    private float groundPos = 1;
    private float downwardsVelocity = -2;
    private Vector3 velocity = Vector3.zero;

    //Variables related to camera
    private Transform cameraTransform;
    public Transform player;
    private float offset = 1.5f;
    private float mouseX;
    private float mouseY;
    public float mouseSensitivity = 5;
    private float cameraYMin = -90;
    private float cameraYMax = 90;

    void Start()
    {
        //Lock cursor to center
        Cursor.lockState = CursorLockMode.Locked;

        //Get CharacterController component
        characterController = GetComponent<CharacterController>();

        //Set camera to first person view
        cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        cameraTransform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Move with WASD or arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Direction = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        velocity.x = moveDirection.x * speed;
        velocity.z = moveDirection.z * speed;

        if (Direction.magnitude > minSpeed)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Direction.magnitude > minSpeed && !isCrouching)
            {
                Sprint();
                moveDirection = Direction;
            }
            else
            {
                speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);
                moveDirection = Direction;
            }
                
        }
        else
        {
            speed = Mathf.MoveTowards(speed, minSpeed, deceleration * Time.deltaTime);
        }

        //Crouch with Ctrl
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                characterController.height = height;
                isCrouching = false;
            }
            else
            {
                characterController.height = crouchHeight;
                isCrouching = true;
            }
        }

            //Move Camera with mouse
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * -mouseSensitivity;

            mouseY = Mathf.Clamp(mouseY, cameraYMin, cameraYMax);

           cameraTransform.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0);
           transform.localRotation *= Quaternion.Euler(0, mouseX, 0);



        //Check if on ground
        bool isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = downwardsVelocity;
        }

        //Jump with Spacebar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            velocity.y = jumpForce;
            Debug.Log("On key press: " + velocity.y);
        }

        //Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Sprint()
    {
        speed = Mathf.MoveTowards(speed, maxSprint, acceleration * Time.deltaTime);
    }
}
