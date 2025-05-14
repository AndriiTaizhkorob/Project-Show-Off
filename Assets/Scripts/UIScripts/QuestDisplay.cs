using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _questText;
    [SerializeField]
    private TextMeshProUGUI _questDescription;

    private Quest _quest;

    public void Init(Quest quest)
    {
        _quest = quest;
        _questText.text = quest.GetStatusText();
        _quest.OnValueChange += OnQuestValueChange;
        _quest.OnComplete += OnQuestComplete;
        _quest.OnDescriptionChanged += OnDescriptionChanged;

        OnDescriptionChanged(_quest.Description);
    }

    private void OnQuestComplete()
    {
        _questText.text = $"<s>{_quest.GetStatusText()}</s>";
    }

    private void OnQuestValueChange()
    {
        _questText.text = _quest.GetStatusText();
    }
    
    private void OnDescriptionChanged(string newDesc)
    {
        _questDescription.text = newDesc;
    }
}
