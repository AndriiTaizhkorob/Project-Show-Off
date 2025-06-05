using UnityEngine;

public class HideAndSeek : MonoBehaviour, IDataPersistence
{
    private GameObject[] tpSpots;
    private GameObject questUI;
    public int currentValue;
    public int spotNumber;
    private int spotLimit;
    private bool inProgress;
    public GameObject teleportEffectPrefab;

    void Start()
    {
        tpSpots = GetComponent<QuestTrigger>().Objects;
        questUI = GetComponent<QuestTrigger>().characterUI;
        spotLimit = GetComponent<QuestTrigger>().itemAmount;
    }

    void Update()
    {
        inProgress = GetComponent<QuestTrigger>().isAccepted;
        currentValue = GetComponent<QuestTrigger>().currentValue;
        var runner = FindAnyObjectByType<Yarn.Unity.DialogueRunner>();
        if (runner != null)
        {
            runner.VariableStorage.SetValue("$potato_progress", spotNumber);
        }

        ForceTeleport();
    }
    public void ForceTeleport()
    {
        if (inProgress && currentValue == spotNumber && currentValue != spotLimit)
        {
            if (teleportEffectPrefab != null)
            {
                Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
            }

            transform.position = tpSpots[currentValue].transform.position;

            if (teleportEffectPrefab != null)
            {
                Instantiate(teleportEffectPrefab, transform.position, Quaternion.identity);
            }

            spotNumber++;
        }
    }


    public void LoadData(GameData data)
    {
        spotNumber = (data.currentSpot > currentValue) ? data.currentSpot - 1 : data.currentSpot;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSpot = spotNumber;
    }
}
