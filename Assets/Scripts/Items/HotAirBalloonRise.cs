using UnityEngine;

public class HotAirBalloonRise : MonoBehaviour, IDataPersistence
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

    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

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

        if(complete)
            endPoint = new Vector3(transform.position.x, targetPosition.y, transform.position.z);

        gameObject.transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed);
    }

    public void Rise(float flameStrength)
    {
        if (gameObject.GetComponent<ProgressManager>().enabled)
            progressScale += flameStrength * Time.deltaTime;
    }

    public void LoadData(GameData data)
    {
        data.balloonProgress.TryGetValue(id, out complete);
    }

    public void SaveData(ref GameData data)
    {
        if (data.balloonProgress.ContainsKey(id))
        {
            data.balloonProgress.Remove(id);
        }
        data.balloonProgress.Add(id, complete);
    }
}
