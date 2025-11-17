using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5;
    public float gravity = -10;

    private Vector3 velocity;

    private CharacterController characterController;
    private Transform cameraTransform;

    void Start()
    {
        //Get CharacterController component
        characterController = GetComponent<CharacterController>();

        //Set camera to first person view
        cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
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
