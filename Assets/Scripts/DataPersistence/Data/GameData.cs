using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<string> monsterNames;
    public List<bool> activeQuests;
    public List<bool> finishedQuests;
    public List<int> questProgresses;
    public SerializableDictionary<string, float> growthProgress;
    public SerializableDictionary<string, bool> balloonProgress;
    public SerializableDictionary<string, bool> collectedItems;

    public GameData()
    {
        this.monsterNames = new();
        this.activeQuests = new();
        this.finishedQuests = new();
        this.questProgresses = new();
        this.growthProgress = new SerializableDictionary<string, float>();
        this.balloonProgress = new SerializableDictionary<string, bool>();
        collectedItems = new SerializableDictionary<string, bool>();
    }
}
