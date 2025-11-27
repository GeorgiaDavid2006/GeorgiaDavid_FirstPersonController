using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonController : MonoBehaviour
{
    private CharacterController characterController;

    //Variables related to movement
    public float minSpeed = 0;
    public float speed;
    public float maxSpeed = 10;
    public float acceleration = 5;
    public float deceleration = 5;

    //Variables related to jumping
    public float jumpForce = 5;
    public float gravity = -5;
    private float groundPos = 1;
    private Vector3 velocity;

    //Variables related to camera
    private Transform cameraTransform;
    public Transform player;
    private float offset = 1.5f;
    public float mouseSensitivity = 5;

    void Start()
    {
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

        Vector3 Direction = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(moveDirection * Time.deltaTime);

        if (Direction.magnitude > minSpeed)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);
            moveDirection = Direction;
        }
        else
        {
            speed = Mathf.MoveTowards(speed, minSpeed, deceleration * Time.deltaTime);
        }

        //Move Camera with mouse
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        //Check if on ground
        bool isGrounded = characterController.isGrounded;
        if (velocity.y <= groundPos)
        {
            isGrounded = true;
            velocity.y = groundPos;
        }
        
        //Jump with Spacebar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            velocity.y = jumpForce;
        }

        //Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
