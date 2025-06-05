using UnityEngine;

public class SceneSpawnManager : MonoBehaviour, IDataPersistence
{
    private string targetDoorID;

    public void LoadData(GameData data)
    {
        targetDoorID = data.lastUsedDoorID;
    }

    public void SaveData(ref GameData data)
    {
        // Nothing to save here
    }

    void Start()
    {
        if (string.IsNullOrEmpty(targetDoorID)) return;

        GameObject player = GameObject.Find("Player");
        if (player == null) return;

        var spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        foreach (var point in spawnPoints)
        {
            if (point.doorID == targetDoorID)
            {
                player.transform.position = point.transform.position;
                player.transform.rotation = point.transform.rotation;
                break;
            }
        }

    }
}
