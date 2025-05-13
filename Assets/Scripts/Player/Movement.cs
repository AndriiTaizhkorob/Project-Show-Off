using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference jump;
    public Rigidbody rb;

    //public CharacterController controller;

    private Vector2 moveDirection;
    private Vector3 velocity;

    public float speed = 1.0f;
    public float jumpForce = 1.0f;
    //public float gravity = -9.81f;

    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if(jump.action.triggered && Physics.Raycast(transform.position, Vector3.down, 1 + 0.01f))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.y);
        }

        moveDirection = move.action.ReadValue<Vector2>();
        velocity = (transform.forward * moveDirection.y * speed + transform.right * moveDirection.x * speed + transform.up * rb.linearVelocity.y);
        rb.linearVelocity = velocity;

        //Movement with player controller, denied.

        //Vector3 movement = transform.right * moveDirection.x + transform.forward * moveDirection.y;

        //controller.Move(speed * Time.deltaTime * movement);

        //velocity.y += gravity * Time.deltaTime;

        //controller.Move(velocity * Time.deltaTime);
    }
}
