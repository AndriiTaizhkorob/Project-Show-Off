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

    private bool complete = false;
    private Vector3 endPoint;

    private void Start()
    {
        endPoint = transform.position;
    }

    void Update()
    {
        progressScale = Mathf.Clamp(progressScale, 0f, progressLimit);

        if (progressScale == progressLimit && !complete)
        {
            gameObject.GetComponent<ProgressManager>().UpdateScore(pickUpValue);
            endPoint = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
            complete = true;
        }

        gameObject.transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed);
    }

    public void Rise(float flameStrength)
    {
        if (gameObject.GetComponent<ProgressManager>().enabled)
            progressScale += flameStrength * Time.deltaTime;
    }
}
