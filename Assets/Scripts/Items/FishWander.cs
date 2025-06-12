using UnityEngine;

public class FishWander : MonoBehaviour
{
    [Header("Movement Settings")]
    public float swimSpeed = 1.5f;
    public float idleTimeMin = 1f;
    public float idleTimeMax = 3f;
    public float lakeRadius = 5f;

    [Header("Speed Variation")]
    public float minSwimSpeed = 1f;
    public float maxSwimSpeed = 2.5f;

    [Header("Reference Point")]
    public Transform centerPoint;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private float idleTimer;
    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y; // Lock Y position
        PickNewTarget();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, swimSpeed * Time.deltaTime);

            // Rotate to face the direction of travel
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Ignore vertical for rotation
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                idleTimer = Random.Range(idleTimeMin, idleTimeMax);
            }
        }
        else
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                PickNewTarget();
            }
        }
    }

    void PickNewTarget()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-lakeRadius, lakeRadius),
            0,
            Random.Range(-lakeRadius, lakeRadius)
        );

        targetPosition = centerPoint.position + randomOffset;
        targetPosition.y = fixedY;

        swimSpeed = Random.Range(minSwimSpeed, maxSwimSpeed);
        isMoving = true;
    }
}
