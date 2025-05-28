using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<GameObject> Monsters;
    public List<bool> activeQuests;
    public List<int> questProgresses;

    public GameData()
    {
        this.Monsters = new();
        this.activeQuests = new();
        this.questProgresses = new();
    }
}
