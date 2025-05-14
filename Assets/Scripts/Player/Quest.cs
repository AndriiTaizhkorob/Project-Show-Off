using System;
using UnityEngine;

public class Quest
{
    public Action OnComplete;
    public Action OnValueChange;
    public Action<string> OnDescriptionChanged;

    public GameObject npc;

    public string EventTrigger { get; }
    public bool IsComplete { get; private set; }
    public int MaxValue { get; }
    public int CurrentValue { get; private set; }

    private readonly string _statusText;
    private string _description;
    public string Description
    {
        get => _description;
        set
        {
            if (_description == value) return;
            _description = value;
            OnDescriptionChanged?.Invoke(_description);
        }
    }

    public Quest(string eventTrigger, string statusText, int maxValue)
    {
        EventTrigger = eventTrigger;
        _statusText = statusText;
        MaxValue = maxValue;
    }

    public Quest(string statusText, int maxValue) : this("", statusText, maxValue) { }

    private void CheckCompletion()
    {
        if (CurrentValue >= MaxValue)
        {
            IsComplete = true;
            OnComplete?.Invoke();
        }
    }

    public void AddProgress(int value)
    {
        if (IsComplete)
        {
            return;
        }
        CurrentValue += value;
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }
        OnValueChange?.Invoke();
        CheckCompletion();
    }

    public string GetStatusText()
    {
        return string.Format(_statusText, CurrentValue, MaxValue);
    }
}
