using UnityEngine;
using UnityEngine.UI;

public class QuestProgressBarUI : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    private Quest quest;

    public void Init(Quest quest)
    {
        this.quest = quest;

        if (progressBar != null)
        {
            UpdateBar();
        }

        quest.OnValueChange += UpdateBar;
        quest.OnComplete += OnQuestComplete;
    }

    private void UpdateBar()
    {
        if (quest == null || progressBar == null)
            return;

        float progress = Mathf.Clamp01((float)quest.CurrentValue / quest.MaxValue);
        progressBar.value = progress;
    }

    private void OnQuestComplete()
    {
        if (progressBar != null)
        {
            progressBar.value = 1f;
        }
    }
}
