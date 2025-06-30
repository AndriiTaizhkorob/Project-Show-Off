using UnityEngine;

public class QuestExclamationIndicator : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject exclamationMark;
    public float floatAmplitude = 0.1f;
    public float floatSpeed = 2f;
    public float yRotationOffset = 90f;

    [Header("Quest Info")]
    public string questName;

    private Vector3 initialPos;
    private Camera mainCamera;
    private QuestTrigger questTrigger;
    private bool isActive;

    void Start()
    {
        if (exclamationMark != null)
        {
            initialPos = exclamationMark.transform.localPosition;
            exclamationMark.SetActive(false);
        }

        mainCamera = Camera.main;
        questTrigger = GetComponent<QuestTrigger>();
    }

    void Update()
    {
        if (questTrigger == null || exclamationMark == null)
            return;

        UpdateIndicatorState();

        if (!isActive) return;

        // Bobbing motion
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        exclamationMark.transform.localPosition = initialPos + new Vector3(0, offset, 0);

        // Face camera
        Vector3 euler = exclamationMark.transform.eulerAngles;
        euler.y = mainCamera.transform.eulerAngles.y + yRotationOffset;
        exclamationMark.transform.eulerAngles = euler;
    }

    void UpdateIndicatorState()
    {
        bool show = (!questTrigger.isAccepted && !questTrigger.isHandedIn) ||
                    (questTrigger.isCompleted && !questTrigger.isHandedIn);

        if (show != isActive)
        {
            isActive = show;
            exclamationMark.SetActive(show);
        }
    }
}

