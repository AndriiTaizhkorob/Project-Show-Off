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
    public GameObject questManager;

    private Camera mainCamera;

    public float checkRadius = 1.0f;

    public int currentValue = 1;

    private bool isCompleted = false;

    [Header("Quest details")]
    public string questName = "Sphere quest";
    public string questDescription;
    public int itemAmount;
    public GameObject[] pickUpItems;

    public string questText;

    public Quest questPreset;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        questManager = GameObject.Find("QuestManager");
        characterUI.SetActive(false);
        completeButton.SetActive(false);

        questManager.GetComponent<QuestManager>().NPC = gameObject;
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

        if(!cameraMove.action.inProgress && characterUI.activeInHierarchy)
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
        questPreset = new Quest(questName, questDescription, itemAmount);
        questManager.GetComponent<QuestManager>().AddQuest(questPreset);
        questManager.GetComponent<QuestManager>().Init(questPreset);

        if(pickUpItems.Length > 0)
        {
            for (int i = 0; i < pickUpItems.Length; i++)
            {
                pickUpItems[i].GetComponent<ItemPickUp>().QuestActive();
            }
        }
        questText = currentValue + "/" + itemAmount;
        questPreset.Description = questText;
        acceptButton.SetActive(false);
    }

    public void QuestUpdate()
    {
        questText = currentValue + "/" + itemAmount;
        questPreset.Description = questText;
    }

    public void QuestComplete()
    {
        isCompleted = true;
        Debug.Log("You got them all!");
    }

    public void QuestHandedIn()
    {
        isCompleted = false;
    }
}
