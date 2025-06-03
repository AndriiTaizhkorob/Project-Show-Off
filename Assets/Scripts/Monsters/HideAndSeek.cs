using UnityEngine;

public class HideAndSeek : MonoBehaviour
{
    public GameObject[] tpSpots;
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
                gameObject.transform.position = tpSpots[spotNumber].transform.position;
                spotNumber++;
            } 
        }
    }
}
