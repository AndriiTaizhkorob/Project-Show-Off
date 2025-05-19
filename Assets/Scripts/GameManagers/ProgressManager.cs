using UnityEngine;

public class ProgressManager : MonoBehaviour
{

    public GameObject npc;
    private GameObject questManager;

    void Awake()
    {
        questManager = GameObject.Find("QuestManager");
        enabled = false;
    }

    public void UpdateScore(int pickUpValue)
    {
        npc.GetComponent<QuestTrigger>().currentValue += pickUpValue;
        questManager.GetComponent<QuestManager>().AddProgress(npc.GetComponent<QuestTrigger>().questName, pickUpValue);
        npc.GetComponent<QuestTrigger>().QuestUpdate();
    }
}
