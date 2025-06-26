using UnityEngine;

public class PenguinDropoffIndicator : MonoBehaviour
{
    public GameObject arrow;
    public string questName = "Penguin search";
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;
    public float yRotationOffset = 90f;

    public PenguinGoalArea goalArea; // Drag this in Inspector
    public int totalPenguins = 3;

    private Vector3 initialArrowPosition;
    private Camera mainCamera;
    private bool isActive;

    void Start()
    {
        if (arrow != null)
        {
            initialArrowPosition = arrow.transform.localPosition;
            arrow.SetActive(false);
        }

        mainCamera = Camera.main;

        QuestManager questManager = FindAnyObjectByType<QuestManager>();
        if (questManager != null)
        {
            questManager.OnQuestAdded += HandleQuestAdded;
        }
    }

    void Update()
    {
        if (!isActive || arrow == null || goalArea == null) return;

        float offset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        arrow.transform.localPosition = initialArrowPosition + new Vector3(0, offset, 0);

        Vector3 euler = arrow.transform.eulerAngles;
        euler.y = mainCamera.transform.eulerAngles.y + yRotationOffset;
        arrow.transform.eulerAngles = euler;

        if (goalArea.rescuedPenguinCount >= totalPenguins)
        {
            arrow.SetActive(false);
            isActive = false;
        }
    }

    void HandleQuestAdded(Quest quest)
    {
        if (quest.EventTrigger == questName && arrow != null)
        {
            arrow.SetActive(true);
            isActive = true;
        }
    }
}

