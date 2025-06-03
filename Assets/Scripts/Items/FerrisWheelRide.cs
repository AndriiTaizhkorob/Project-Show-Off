using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FerrisWheelRide : MonoBehaviour
{
    public Transform player;
    public Transform rideCart;
    public FerrisWheelRotator wheel;
    public InputActionReference interactAction;
    public float fullRotationDegrees = 360f;
    public float interactionRadius = 2f;
    public Vector3 playerRotationOffset;
    public Vector3 playerPositionOffset;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Quaternion originalWheelRotation;
    private bool rideInProgress = false;
    private float startAngle;

    void Start()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
        }

        if (wheel != null && wheel.wheel != null)
        {
            originalWheelRotation = wheel.wheel.localRotation;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!rideInProgress && distance <= interactionRadius)
        {
            if (interactAction != null && interactAction.action.WasPressedThisFrame())
            {
                StartRide();
            }
        }
    }

    public void StartRide()
    {
        if (rideInProgress)
            return;

        if (player == null || rideCart == null || wheel == null)
            return;

        rideInProgress = true;

        originalPosition = player.position;
        originalRotation = player.rotation;

        var rb = player.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        var movementScript = player.GetComponent<Movement>();
        if (movementScript != null)
            movementScript.enabled = false;

        player.SetParent(rideCart);
        player.localPosition = playerPositionOffset;
        player.localRotation = Quaternion.identity;
        player.Rotate(playerRotationOffset, Space.Self);

        if (wheel.wheel != null)
        {
            Vector3 euler = originalWheelRotation.eulerAngles;
            wheel.wheel.localRotation = Quaternion.Euler(euler.x, 0f, euler.z);
        }

        startAngle = 0f;

        StartCoroutine(WaitForFullRotation());
    }

    IEnumerator WaitForFullRotation()
    {
        float degreesTraveled = 0f;
        float lastAngle = startAngle;

        while (degreesTraveled < fullRotationDegrees)
        {
            float currentAngle = wheel.wheel.localEulerAngles.y;
            float delta = Mathf.DeltaAngle(lastAngle, currentAngle);
            degreesTraveled += Mathf.Abs(delta);
            lastAngle = currentAngle;

            yield return null;
        }

        EndRide();
    }

    void EndRide()
    {
        player.SetParent(null);
        player.position = originalPosition;
        player.rotation = originalRotation;

        var rb = player.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        var movementScript = player.GetComponent<Movement>();
        if (movementScript != null)
            movementScript.enabled = true;

        rideInProgress = false;
    }
}
