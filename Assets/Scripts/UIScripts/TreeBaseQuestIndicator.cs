using UnityEngine;
using UnityEngine.InputSystem;

public class TreeBaseQuestIndicator : MonoBehaviour
{
    public GameObject arrow;
    public string questName;
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;
    public float yRotationOffset = 90f;

    private Vector3 initialArrowPosition;
    private bool isActive;
    private Camera mainCamera;
    private TreeGrowth treeGrowth;

    void Start()
    {
        if (arrow != null)
        {
            initialArrowPosition = arrow.transform.localPosition;
            arrow.SetActive(false);
        }

        mainCamera = Camera.main;
        treeGrowth = GetComponent<TreeGrowth>();

        QuestManager questManager = FindAnyObjectByType<QuestManager>();
        if (questManager != null)
        {
            questManager.OnQuestAdded += HandleQuestAdded;
        }
    }

    void Update()
    {
        if (!isActive || arrow == null) return;

        float offset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        arrow.transform.localPosition = initialArrowPosition + new Vector3(0, offset, 0);

        Vector3 euler = arrow.transform.eulerAngles;
        euler.y = mainCamera.transform.eulerAngles.y + yRotationOffset;
        arrow.transform.eulerAngles = euler;

        if (treeGrowth != null && IsTreeSpawned())
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

    bool IsTreeSpawned()
    {
        return typeof(TreeGrowth).GetField("treeObj", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               .GetValue(treeGrowth) is GameObject tree && tree != null;
    }

    public void HideArrow()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
            isActive = false;
        }
    }
}
