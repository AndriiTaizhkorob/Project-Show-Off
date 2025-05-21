using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuestTrigger : MonoBehaviour
{
    public InputActionReference interaction;
    public InputActionReference cameraMove;

    public LayerMask playerMask;

    public GameObject characterUI;
    public GameObject completeObj;
    public GameObject closeObj;
    public GameObject acceptObj;
    public GameObject player;
    public GameObject questManager;
    public Button completeButton;
    public Button acceptButton;
    public Button closeButton;

    public float checkRadius = 1.0f;

    public int currentValue = 0;

    private bool isCompleted = false;
    private bool isAccepted = false;
    public bool delayed = true;

    [Header("Quest details")]
    public string questName = "Sphere quest";
    public string questDescription;
    public int itemAmount;
    public GameObject[] Objects;

    public string questText;

    public Quest questPreset;

    public bool questAssigned = false;

    void Start()
    {
        player = GameObject.Find("Player");
        questManager = GameObject.Find("QuestManager");
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
