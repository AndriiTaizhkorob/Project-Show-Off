using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    public InputActionReference interaction;
    public InputActionReference cameraMove;

    public LayerMask playerMask;

    public GameObject characterUI;
    public GameObject completeButton;
    public GameObject acceptButton;
    public GameObject player;

    private Camera mainCamera;

    public float checkRadius = 1.0f;

    public int currentValue;

    private bool isCompleted = false;

    [Header("Quest details")]
    public string questName = "Sphere quest";
    public string questDescription;
    public int itemAmount;

    public Quest questPreset;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        characterUI.SetActive(false);
        completeButton.SetActive(false);

        player.GetComponent<QuestManager>().NPC = gameObject;
    }

    void Update()
    {
        if (interaction.action.triggered)
        {
            NPCInteraction();
        }
        if (!Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            characterUI.SetActive(false);
        }

        if (cameraMove.action.inProgress && characterUI.activeInHierarchy)
        {
            mainCamera.GetComponent<CameraControls>().activeControls = true;
        }
        else if(!cameraMove.action.inProgress && characterUI.activeInHierarchy)
        {
            mainCamera.GetComponent<CameraControls>().activeControls = false;
        }
        else
        {
            mainCamera.GetComponent<CameraControls>().activeControls = true;
        }
    }

    public void NPCInteraction()
    {
        if (Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            characterUI.SetActive(true);
        }

        if (isCompleted)
        {
            completeButton.SetActive(true);
        }
    }

    public void QuestStart()
    {
        Quest questPreset = new Quest(questName, questDescription, itemAmount);
        player.GetComponent<QuestManager>().AddQuest(questPreset);
        player.GetComponent<QuestManager>().Init(questPreset);

        acceptButton.SetActive(false);
    }

    public void QuestComplete()
    {
        isCompleted = true;
        Debug.Log("Quest complete!");
    }
}
