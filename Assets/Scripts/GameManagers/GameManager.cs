using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public QuestManager QuestManager { get; private set; }

    public List<Quest> Quests => QuestManager.Quests;

    public GameObject questManager;

    void Awake()
    {
        questManager = GameObject.Find("QuestManager");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        QuestManager = questManager.GetComponent<QuestManager>();
        if (QuestManager == null)
            QuestManager = gameObject.AddComponent<QuestManager>();
    }
}
