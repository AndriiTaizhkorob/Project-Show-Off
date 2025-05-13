using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    public InputActionReference interaction;

    public float checkRadius = 1.0f;

    private bool isActive = false;

    public LayerMask playerMask;

    public GameObject player;
    public GameObject npc;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (interaction.action.triggered && Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        player.GetComponent<QuestManager>().AddProgress(npc.GetComponent<QuestTrigger>().questName, 1);
        Debug.Log("Got the sphere.");
        Destroy(gameObject);
    }

    public void QuestActive()
    {
        isActive = true;
    }
}
