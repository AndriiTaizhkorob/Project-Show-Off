using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickUp : MonoBehaviour, IDataPersistence
{
    public InputActionReference interaction;

    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public float checkRadius = 1.0f;
    public int pickUpValue = 1;

    public LayerMask playerMask;

    private bool isCollected = false;

    void Update()
    {
        if (interaction.action.triggered && Physics.CheckSphere(transform.position, checkRadius, playerMask) && gameObject.GetComponent<ProgressManager>().enabled)
        {
            gameObject.GetComponent<ProgressManager>().UpdateScore(pickUpValue);
            isCollected = true;
        }

        if(isCollected)
            gameObject.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        data.collectedItems.TryGetValue(id, out isCollected);
    }

    public void SaveData(ref GameData data)
    {
        if (data.collectedItems.ContainsKey(id))
        {
            data.collectedItems.Remove(id);
        }
        data.collectedItems.Add(id, isCollected);
    }
}
