using UnityEngine;

public class HideAndSeek : MonoBehaviour, IDataPersistence
{
    private GameObject[] tpSpots;
    private GameObject questUI;
    public int currentValue;
    public int spotNumber;
    private int spotLimit;
    private bool inProgress;


    void Start()
    {
        tpSpots = gameObject.GetComponent<QuestTrigger>().Objects;
        questUI = gameObject.GetComponent<QuestTrigger>().characterUI;
        spotLimit = gameObject.GetComponent<QuestTrigger>().itemAmount;
    }

    void Update()
    {
        inProgress = gameObject.GetComponent<QuestTrigger>().isAccepted;
        currentValue = gameObject.GetComponent<QuestTrigger>().currentValue;

        Teleport();
    }

    public void Teleport()
    {
        if (inProgress)
        {        
            if (currentValue == spotNumber && !questUI.activeInHierarchy && currentValue != spotLimit)
            {
                gameObject.transform.position = tpSpots[currentValue].transform.position;
                spotNumber++;
            } 
        }
    }

    public void LoadData(GameData data)
    {
        if(data.currentSpot > 0)
            spotNumber = data.currentSpot - 1;
        else
            spotNumber = data.currentSpot;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSpot = spotNumber;
    }
}
