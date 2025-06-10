using UnityEngine;

public class SceneLoader : MonoBehaviour, IDataPersistence
{
    public string nextSceneName;
    public string destinationDoorID;
    public float reloadtime = 0f;

    private string pendingDoorID;

    private void Awake()
    {
        GameObject.Find("characterUI")?.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pendingDoorID = destinationDoorID;
            DataPersistenceManager.Instance.SaveGame();
            LoadingScreenManager.Instance.SwitchToScene(nextSceneName);
        }
    }

    public void LoadData(GameData data)
    {
        // Not needed for this script
    }

    public void SaveData(ref GameData data)
    {
        if (!string.IsNullOrEmpty(pendingDoorID))
        {
            data.lastUsedDoorID = pendingDoorID;
        }
    }
}
