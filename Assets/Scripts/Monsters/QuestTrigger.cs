using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Yarn.Unity;

public class QuestTrigger : MonoBehaviour, IDataPersistence
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
    private GameObject dataPersistenceManager;

    private Button completeButton;
    private Button acceptButton;
    private Button closeButton;

    [Header("Interaction Radius")]
    public float checkRadius = 1.0f;

    public bool isCompleted = false;
    [HideInInspector] public bool isAccepted = false;
    private bool isHandedIn = false;
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
    public string dialogueNode;
    private DialogueRunner dialogueRunner;

    void Awake()
    {
        player = GameObject.Find("Player");
        questManager = GameObject.Find("QuestManager");
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");
        dialogueRunner = Object.FindFirstObjectByType<DialogueRunner>();

        completeButton = completeObj.GetComponent<Button>();
        acceptButton = acceptObj.GetComponent<Button>();
        closeButton = closeObj.GetComponent<Button>();
    }

    void Start()
    {
        characterUI.SetActive(false);
        completeObj.SetActive(false);
    }

    void Update()
    {
        if (interaction.action.triggered && delayed && IsPlayerNearby() && dialogueNode.Length > 0)
        {
            StartDialogue();
        }
        else if (interaction.action.triggered && delayed && IsPlayerNearby() && dialogueNode.Length == 0)
        {
            NPCInteraction();
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

    [Header("Dialogue")]
    public string dialogueStartNode;
    public string dialogueInProgressNode;
    public string dialogueCompleteNode;

    [SerializeField] private Movement playerMovement;
    private void StartDialogue()
    {
        if (dialogueRunner != null && !dialogueRunner.IsDialogueRunning)
        {
            string nodeToRun = dialogueStartNode;

            if (questPreset != null)
            {
                if (questPreset.IsComplete && !string.IsNullOrEmpty(dialogueCompleteNode))
                {
                    nodeToRun = dialogueCompleteNode;
                }
                else if (isAccepted && !string.IsNullOrEmpty(dialogueInProgressNode))
                {
                    nodeToRun = dialogueInProgressNode;
                }
            }

            // Disable movement
            if (playerMovement != null)
                playerMovement.enabled = false;

            // Start dialogue
            dialogueRunner.StartDialogue(nodeToRun);

            // Re-enable movement when dialogue ends
            dialogueRunner.onDialogueComplete.AddListener(() =>
            {
                if (playerMovement != null)
                    playerMovement.enabled = true;
            });
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

        //For testing
        if (IsPlayerNearby())
        {
            delayed = false;
            characterUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeObj);

            if (isAccepted)
            {
                acceptObj.SetActive(false);
            }
            else
            {
                acceptObj.SetActive(true);
            }

            if (isCompleted)
            {
                completeObj.SetActive(true);
            }


            if(!isCompleted)
            {
                completeObj.SetActive(false);
                EventSystem.current.SetSelectedGameObject(closeObj);
            }
        }

        characterUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeObj);

        acceptObj.SetActive(!isAccepted);
        completeObj.SetActive(isCompleted);
    }

    public void QuestStart()
    {
        questPreset = new Quest(questName, questDescription, itemAmount);
        questManager.GetComponent<QuestManager>().AddQuest(questPreset);
        questManager.GetComponent<QuestManager>().Init(questPreset);

        foreach (var obj in Objects)
        {
            if (obj != null && obj.GetComponent<ProgressManager>() != null)
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

        yield return new WaitForSeconds(3f);
        delayed = true;
    }

    public void QuestUpdate()
    {
        if (currentValue > itemAmount)
            currentValue = itemAmount;

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
        player.GetComponent<QuestPanel>().ResetCurrent("<s>" + questDescription + "</s>");
    }

    public void LoadData(GameData data)
    {
        for(int i = 0; i < data.monsterNames.Count; i++)
        {
            if (data.monsterNames[i] == this.gameObject.name)
            {
                isAccepted = data.activeQuests[i];
                currentValue = data.questProgresses[i];
                isHandedIn = data.finishedQuests[i];

                if (isAccepted && !isHandedIn)
                {
                    QuestStart();
                    questManager.GetComponent<QuestManager>().NPC = this.gameObject;
                    questManager.GetComponent<QuestManager>().AddProgress(questName, currentValue);
                }

                break;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (!data.monsterNames.Contains(this.gameObject.name))
        {
            data.monsterNames.Add(this.gameObject.name);
            data.activeQuests.Add(isAccepted);
            data.questProgresses.Add(currentValue);
            data.finishedQuests.Add(isHandedIn);
        }
        else
        {
            for (int i = 0; i < data.monsterNames.Count; i++)
            {
                if (data.monsterNames[i] == gameObject.name)
                {
                    data.activeQuests[i] = isAccepted;
                    data.questProgresses[i] = currentValue;
                    data.finishedQuests[i] = isHandedIn;

                    break;
                }
            }
        }
    }
}