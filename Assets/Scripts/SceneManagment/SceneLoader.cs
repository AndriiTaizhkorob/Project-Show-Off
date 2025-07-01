using UnityEngine;

public class SceneLoader : MonoBehaviour, IDataPersistence
{
    public string nextSceneName;
    public string destinationDoorID;
    public float reloadtime = 0f;

    private string pendingDoorID;

    private GameObject player;

    private void Awake()
    {
        GameObject.Find("characterUI")?.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        player = GameObject.Find("Player");

        if (other.CompareTag("Player"))
        {
            player.GetComponent<Movement>().StopSound();
            player.GetComponent<FirePower>().StopSound();
            player.GetComponent<IcePower>().StopSound();
            player.GetComponent<Movement>().enabled = false;
            pendingDoorID = destinationDoorID;
            DataPersistenceManager.Instance.SaveGame();
            LoadingScreenManager.Instance.SwitchToScene(nextSceneName, reloadtime);
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
