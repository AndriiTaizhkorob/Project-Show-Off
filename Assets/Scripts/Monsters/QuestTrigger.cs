using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Yarn.Unity;

public class QuestTrigger : MonoBehaviour
{
    [Header("Player Input")]
    public InputActionReference interaction;
    public InputActionReference cameraMove;
    public LayerMask playerMask;

    [Header("UI elements")]
    public GameObject characterUI;
    public GameObject completeObj;
    public GameObject acceptObj;
    public GameObject closeObj;

    private GameObject questManager;

    private Button completeButton;
    private Button acceptButton;
    private Button closeButton;

    [Header("Interaction Radius")]
    public float checkRadius = 1.0f;

    private bool isCompleted = false;
    private bool isAccepted = false;
    private bool delayed = true;

    [Header("Quest details")]
    public string questName = "Sphere quest";
    public string questDescription;
    public int itemAmount;
    public GameObject[] Objects;

    [HideInInspector] public GameObject player;
    [HideInInspector] public string questText;
    [HideInInspector] public Quest questPreset;
    [HideInInspector] public int currentValue = 0;
    [HideInInspector] public bool questAssigned = false;

    [Header("Dialogue")]
    public string dialogueNode = "Kitty";
    private DialogueRunner dialogueRunner;

    void Start()
    {
        player = GameObject.Find("Player");
        questManager = GameObject.Find("QuestManager");
        dialogueRunner = Object.FindFirstObjectByType<DialogueRunner>();

        completeButton = completeObj.GetComponent<Button>();
        acceptButton = acceptObj.GetComponent<Button>();
        closeButton = closeObj.GetComponent<Button>();

        characterUI.SetActive(false);
        completeObj.SetActive(false);
    }

    void Update()
    {
        if (interaction.action.triggered && delayed && IsPlayerNearby())
        {
            StartDialogue();
        }

        if (!IsPlayerNearby())
        {
            acceptButton.onClick.RemoveListener(QuestStart);
            completeButton.onClick.RemoveListener(QuestHandedIn);
            questAssigned = false;
        }
    }

    private bool IsPlayerNearby()
    {
        return Physics.CheckSphere(transform.position, checkRadius, playerMask);
    }

    private void StartDialogue()
    {
        if (dialogueRunner != null && !dialogueRunner.IsDialogueRunning)
        {
            string nodeToRun = dialogueNode;

            if (questPreset != null)
            {
                if (questPreset.IsComplete)
                {
                    nodeToRun = "Kitty_Complete";
                }
                else if (isAccepted)
                {
                    nodeToRun = "Kitty_InProgress";
                }
            }

            dialogueRunner.StartDialogue(nodeToRun);
        }
    }


    public void NPCInteraction()
    {
        if (!questAssigned)
        {
            acceptButton.onClick.AddListener(QuestStart);
            completeButton.onClick.AddListener(QuestHandedIn);
            closeButton.onClick.AddListener(CloseQuest);

            questAssigned = true;
        }

        characterUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeObj);

        acceptObj.SetActive(!isAccepted);
        completeObj.SetActive(isCompleted);
    }

    public void QuestStart()
    {
        questManager.GetComponent<QuestManager>().NPC = gameObject;

        questPreset = new Quest(questName, questDescription, itemAmount);
        questManager.GetComponent<QuestManager>().AddQuest(questPreset);
        questManager.GetComponent<QuestManager>().Init(questPreset);

        foreach (var obj in Objects)
        {
            if (obj != null)
            {
                var progress = obj.GetComponent<ProgressManager>();
                if (progress != null)
                {
                    progress.enabled = true;
                    progress.npc = gameObject;
                }
            }
        }

        questText = currentValue + "/" + itemAmount;
        questPreset.Description = questText;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeObj);
        isAccepted = true;
    }

    public void CloseQuest()
    {
        closeButton.onClick.RemoveListener(CloseQuest);
        acceptButton.onClick.RemoveListener(QuestStart);
        completeButton.onClick.RemoveListener(QuestHandedIn);

        questAssigned = false;
        EventSystem.current.SetSelectedGameObject(null);
        characterUI.SetActive(false);

        StartCoroutine(DelayActivation());
    }

    IEnumerator DelayActivation()
    {
        yield return new WaitForSeconds(1f);
        delayed = true;
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeObj);
        isCompleted = false;
    }
}

