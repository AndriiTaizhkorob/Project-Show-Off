using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [SerializeField]
    private QuestDisplay _questDisplayPrefab;

    [SerializeField]
    private Transform _questDisplayParent;

    private readonly List<QuestDisplay> _listDisplay = new();

    void Start()
    {
        foreach (var quest in GameManager.Instance.QuestManager.Quests)
        {
            AddObjective(quest);
        }
        GameManager.Instance.QuestManager.OnQuestAdded += AddObjective;
    }

    private void AddObjective(Quest _quest)
    {
        var display = Instantiate(_questDisplayPrefab, _questDisplayParent);
        display.Init(_quest);
        _listDisplay.Add(display);
    }

    public void Reset()
    {
        for (var i = _listDisplay.Count - 1; i >= 0; i--)
        {
            Destroy(_listDisplay[i].gameObject);
        }
        _listDisplay.Clear();
    }
}
