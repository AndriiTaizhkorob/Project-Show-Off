using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterCounterForUI : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject textField;
    public List<string> monsterNames;
    public List<GameObject> currentMonsters;

    private List<bool> finishedQuests;
    private List<string> _monsterNames;
    private int lettersDelivered = 0;
    private int x, y = 0;

    void Update()
    {
        GetNumberOfLettersdelivered();

        textField.GetComponent<TextMeshProUGUI>().text = "" + lettersDelivered + " / " + 5;
    }

    public void LoadData(GameData data)
    {
        _monsterNames = data.monsterNames;
        finishedQuests = data.finishedQuests;

        foreach(String monster in monsterNames)
        {
            GetTheMonster(monster);
        }

        foreach (bool quest in data.finishedQuests)
        {
            if (quest)
                x += 1;
        }
    }

    public void SaveData(ref GameData data)
    {

    }

    private void GetTheMonster(string name)
    {
        if(GameObject.Find(name))
        {
            currentMonsters.Add(GameObject.Find(name));
        }
    }

    private void GetNumberOfLettersdelivered()
    {
        foreach(GameObject monster in currentMonsters)
        {
            if (monster.GetComponent<QuestTrigger>().isHandedIn && !IsSaved(monster.name))
                y += 1;
        }

        lettersDelivered = x + y;
        y = 0;
    }

    private bool IsSaved(string name)
    {
        if (_monsterNames.Count > 0)
        {
            for (int i = 0; i < _monsterNames.Count - 1; i++)
            {
                if (name == _monsterNames[i] && finishedQuests[i])
                {
                    return true;
                }
                else if (name == _monsterNames[i] && !finishedQuests[i])
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }

        return false;
    }
}
