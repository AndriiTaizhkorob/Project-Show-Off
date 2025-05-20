using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference jump;
    public Rigidbody rb;

    private Vector2 moveDirection;
    private Vector3 velocity;

    public GameObject characterUI;

    public float speed = 1.0f;
    public float jumpForce = 1.0f;
  
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (!characterUI.activeInHierarchy)
        {
            Moving();
        }
    }

    public void Moving()
    {
        if (jump.action.triggered && Physics.Raycast(transform.position, Vector3.down, 1 + 0.1f))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.y);
        }

        moveDirection = move.action.ReadValue<Vector2>();
        velocity = (transform.forward * moveDirection.y * speed + transform.right * moveDirection.x * speed + transform.up * rb.linearVelocity.y);
        rb.linearVelocity = velocity;
    }
}
