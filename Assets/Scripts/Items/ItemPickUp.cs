using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    public InputActionReference interaction;

    public float checkRadius = 1.0f;
    public int pickUpValue = 1;

    private bool isActive = false;

    public LayerMask playerMask;

    public GameObject questManager;
    public GameObject npc;

    void Start()
    {
        questManager = GameObject.Find("QuestManager");
    }

    void Update()
    {
        if (interaction.action.triggered && Physics.CheckSphere(transform.position, checkRadius, playerMask) && isActive)
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        npc.GetComponent<QuestTrigger>().currentValue += pickUpValue;
        questManager.GetComponent<QuestManager>().AddProgress(npc.GetComponent<QuestTrigger>().questName, pickUpValue);
        npc.GetComponent<QuestTrigger>().QuestUpdate();
        Destroy(gameObject);
    }

    public void QuestActive()
    {
        isActive = true;
    }
}
