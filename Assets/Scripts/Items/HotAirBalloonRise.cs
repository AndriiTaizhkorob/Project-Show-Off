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

    [SerializeField] private Renderer flameRenderer;
    [SerializeField] private Color startColor = Color.yellow;
    [SerializeField] private Color endColor = Color.red;

    [SerializeField] private Transform flameObject; // The mesh itself
    [SerializeField] private Vector3 minFlameScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 maxFlameScale = new Vector3(2f, 2f, 2f);

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

        if (flameRenderer != null)
        {
            float t = progressScale / progressLimit; // Normalize
            Color currentColor = Color.Lerp(startColor, endColor, t);

            // Change base color
            flameRenderer.material.SetColor("_Color", currentColor);

            // Optional: make the flame glow
            if (flameRenderer.material.IsKeywordEnabled("_EMISSION"))
            {
                flameRenderer.material.SetColor("_EmissionColor", currentColor * 8f);
            }
        }

        if (flameObject != null)
        {
            float t = progressScale / progressLimit;
            Vector3 currentScale = Vector3.Lerp(minFlameScale, maxFlameScale, t);
            flameObject.localScale = currentScale;
        }
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
