using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Action<Quest> OnQuestAdded;

    public List<Quest> Quests { get; } = new();
    private readonly Dictionary<string, List<Quest>> _questMap = new();

    public GameObject NPC;

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
        if (!string.IsNullOrEmpty(quest.EventTrigger))
        {
            if (!_questMap.ContainsKey(quest.EventTrigger))
            {
                _questMap.Add(quest.EventTrigger, new List<Quest>());
            }
            _questMap[quest.EventTrigger].Add(quest);
        }
        OnQuestAdded?.Invoke(quest);
    }

    public void AddProgress(string eventTrigger, int value)
    {
        if (!_questMap.ContainsKey(eventTrigger))
        {
            return;
        }
            
        foreach (var quest in _questMap[eventTrigger])
        {
            quest.AddProgress(value);
        }
    }

    public void Init(Quest quest)
    {
        quest.OnValueChange += OnQuestValueChange;
        quest.OnComplete += OnQuestComplete;
    }

    private void OnQuestComplete()
    {
        NPC.GetComponent<QuestTrigger>().QuestComplete();
    }    
    
    private void OnQuestValueChange()
    {
        Debug.Log("+1");
    }
}
