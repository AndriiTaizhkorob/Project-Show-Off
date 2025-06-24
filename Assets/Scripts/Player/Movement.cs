using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

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

    private EventInstance playerFootsteps;

    private bool isGrounded;
  
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        characterUI = GameObject.Find("characterUI");
    }

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.footSteps);
    }

    void Update()
    {
        if (!characterUI.activeInHierarchy)
        {
            Moving();
            UpdateSound();
        }

        if(Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }
    }

    public void Moving()
    {
        if (!rb.isKinematic)
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

    private void UpdateSound()
    {
        var surfaceIndex = TerrainSurface.GetMainTexture(transform.position);
        playerFootsteps.setParameterByName("SurfaceType", surfaceIndex);

        if (move.action.inProgress && isGrounded)
        {
            playerFootsteps.setParameterByName("isLooping", 1);

            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.setParameterByName("isLooping", 0);
        }
    }

    public void StopSound()
    {
        playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
