using UnityEngine;

public class HotAirBalloonRise : MonoBehaviour
{
    [SerializeField]
    private int pickUpValue;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float progressLimit;
    [SerializeField]
    private float progressScale;
    [SerializeField]
    private Vector3 targetPosition;

    void Update()
    {
        if (progressScale >= progressLimit)
        {
            gameObject.GetComponent<ProgressManager>().UpdateScore(pickUpValue);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
        }
    }

    public void Rise(float flameStrength)
    {
        progressScale += flameStrength * Time.deltaTime;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 3f);
    }
}
