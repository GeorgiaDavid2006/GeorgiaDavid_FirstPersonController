using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonController : MonoBehaviour
{
    private CharacterController characterController;

    //Variables related to movement
    public float speed = 5;

    //Variables related to jumping
    public float gravity = -10;
    private Vector3 velocity;

    //Variables related to camera
    private Transform cameraTransform;
    private float offset = 1.5f;

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
        //Move with WASD
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move * Time.deltaTime);
        
        //Jump with Spacebar
        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            
        }
    }
}
