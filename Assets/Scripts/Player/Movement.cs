using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class Movement : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference jump;
    private Rigidbody rb;

    private Vector2 moveDirection;
    private Vector3 velocity;

    public GameObject characterUI;
    private GameObject endUI;

    public float speed = 1.0f;
    public float jumpForce = 1.0f;

    private EventInstance playerFootsteps;
    private EventInstance playerJump;
    private EventInstance playerLanding;

    private bool isGrounded;
    private bool landed = true;
    [HideInInspector]
    public bool inWater = false;
  
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        characterUI = GameObject.Find("characterUI");
        endUI = GameObject.Find("EndUI");
    }

    private void Start()
    {
        endUI.SetActive(false);
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.footSteps);
        playerJump = AudioManager.instance.CreateInstance(FMODEvents.instance.jump);
        playerLanding = AudioManager.instance.CreateInstance(FMODEvents.instance.landing);
    }

    void Update()
    {
        if (!characterUI.activeInHierarchy && !endUI.activeInHierarchy)
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
                landed = false;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.y);
                playerJump.start();
            }

            moveDirection = move.action.ReadValue<Vector2>();
            velocity = (moveDirection.y * speed * transform.forward + transform.right * moveDirection.x * speed + transform.up * rb.linearVelocity.y);
            rb.linearVelocity = velocity;
        }
    }

    private void UpdateSound()
    {
        var surfaceIndex = 0;

        if (!inWater)
        {
            surfaceIndex = TerrainSurface.GetMainTexture(transform.position);
        }
        else
        {
            surfaceIndex = 9;
        }

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

    private void OnCollisionEnter()
    {
        if (!landed)
        {
            playerLanding.start();
            landed = true;
        }
    }
}
