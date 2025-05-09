using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    public InputActionReference interaction;

    public LayerMask playerMask;

    public GameObject characterUI;

    public float checkRadius = 1.0f;

    void Start()
    {
        characterUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.action.triggered)
        {
            QuestStart();
        }
        if (!Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            characterUI.SetActive(false);
        }
    }

    public void QuestStart()
    {
        if (Physics.CheckSphere(transform.position, checkRadius, playerMask))
        {
            characterUI.SetActive(true);
        }
    }
}
