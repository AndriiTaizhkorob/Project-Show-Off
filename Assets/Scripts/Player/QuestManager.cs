using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Action<Quest> OnQuestAdded;

    private TextMeshProUGUI _questText;

    private Quest _quest;

    public List<Quest> Quests { get; set; } = new();
    private readonly Dictionary<string, List<Quest>> _questMap = new();

    public GameObject NPC;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }  

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

    public bool HasActiveQuest(string questName)
    {
        foreach (var q in Quests)
        {
            if (q.EventTrigger == questName && !q.IsComplete)
                return true;
        }
        return false;
    }

    public void Init(Quest quest)
    {
        _quest = quest;
        _quest.OnValueChange += OnQuestValueChange;
        _quest.OnComplete += OnQuestComplete;
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
