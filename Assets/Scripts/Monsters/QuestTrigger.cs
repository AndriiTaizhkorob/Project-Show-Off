using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    public InputActionReference interaction;

    public LayerMask playerMask;

    public GameObject characterUI;
    public GameObject completeButton;
    public GameObject acceptButton;
    public GameObject player;

    public float checkRadius = 1.0f;

    public int currentValue;

    private bool isCompleted = false;

    [Header("Quest details")]
    public string questName;
    public string questDescription;
    public int itemAmount;

    public Quest questPreset;

    void Start()
    {
        player = GameObject.Find("Player");
        characterUI.SetActive(false);
        completeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.action.triggered)
        {
            NPCInteraction();
        }
        if (!Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            Cursor.lockState = CursorLockMode.Locked;
            characterUI.SetActive(false);
        }
    }

    public void NPCInteraction()
    {
        if (Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            Cursor.lockState = CursorLockMode.None;
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
        acceptButton.SetActive(false);
    }

    public void QuestComplete()
    {
        isCompleted = true;
        Finish();
    }

    public void Finish()
    {
        Debug.Log("Quest complete!");
    }
}
