using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

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

    [Header("Interection Radius")]
    public float checkRadius = 1.0f;

    private bool isCompleted = false;
    private bool isAccepted = false;
    private bool delayed = true;

    [Header("Quest details")]
    public string questName = "Sphere quest";
    public string questDescription;
    public int itemAmount;
    public GameObject[] Objects;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public string questText;
    [HideInInspector]
    public Quest questPreset;
    [HideInInspector]
    public int currentValue = 0;
    [HideInInspector]
    public bool questAssigned = false;

    void Start()
    {
        player = GameObject.Find("Player");
        questManager = GameObject.Find("QuestManager");

        completeButton = completeObj.GetComponent<Button>();
        acceptButton = acceptObj.GetComponent<Button>();
        closeButton = closeObj.GetComponent<Button>();

        characterUI.SetActive(false);
        completeObj.SetActive(false);
    }

    void Update()
    {
        if (interaction.action.triggered && !characterUI.activeInHierarchy && delayed)
        {

            NPCInteraction();
        }
        if (!Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            acceptButton.onClick.RemoveListener(QuestStart);
            completeButton.onClick.RemoveListener(QuestHandedIn);
            questAssigned = false;
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

        if (Physics.CheckSphere(transform.position, checkRadius, playerMask))
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
            else
            {
                completeObj.SetActive(false);
                EventSystem.current.SetSelectedGameObject(closeObj);
            }
        }
    }

    public void QuestStart()
    {
        questManager.GetComponent<QuestManager>().NPC = gameObject;

        questPreset = new Quest(questName, questDescription, itemAmount);
        questManager.GetComponent<QuestManager>().AddQuest(questPreset);
        questManager.GetComponent<QuestManager>().Init(questPreset);

        if (Objects.Length > 0)
        {
            for(int i  = 0; i < Objects.Length; i++)
            {
                Objects[i].GetComponent<ProgressManager>().enabled = true;
                Objects[i].GetComponent<ProgressManager>().npc = gameObject;
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

        StartCoroutine(DelayActivaton());
    }

    IEnumerator DelayActivaton()
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
