using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour
{
    public InputActionReference interaction;

    public float checkRadius = 1.0f;
    public int pickUpValue = 1;

    public LayerMask playerMask;

    void Start()
    {

    }

    void Update()
    {
        if (interaction.action.triggered && Physics.CheckSphere(transform.position, checkRadius, playerMask) && gameObject.GetComponent<ProgressManager>().enabled)
        {
            gameObject.GetComponent<ProgressManager>().UpdateScore(pickUpValue);
            Destroy(gameObject);
        }
    }
}
