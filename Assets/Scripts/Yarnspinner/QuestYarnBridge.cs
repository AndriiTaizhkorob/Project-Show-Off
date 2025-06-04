using UnityEngine;
using Yarn.Unity;

public class QuestYarnBridge : MonoBehaviour
{
    public QuestManager questManager;

    private void Start()
    {
        var runner = Object.FindFirstObjectByType<DialogueRunner>();
        if (runner != null)
        {
            runner.AddCommandHandler<string>("start_quest", StartQuestFromDialogue);
            runner.AddCommandHandler<string>("complete_quest", CompleteQuestFromDialogue);
            runner.AddCommandHandler("teleport_potato", TeleportHideAndSeek);
        }
    }

    public void StartQuestFromDialogue(string questName)
    {
        Debug.Log($"[Yarn] Starting quest: {questName}");

        var allTriggers = Object.FindObjectsByType<QuestTrigger>(FindObjectsSortMode.None);
        foreach (var trigger in allTriggers)
        {
            if (trigger.questName == questName)
            {
                trigger.QuestStart();
                return;
            }
        }

        Debug.LogWarning($"[Yarn] No QuestTrigger found named: {questName}");
    }
    public void CompleteQuestFromDialogue(string questName)
    {
        Debug.Log($"[Yarn] Handing in quest: {questName}");

        var allTriggers = Object.FindObjectsByType<QuestTrigger>(FindObjectsSortMode.None);
        foreach (var trigger in allTriggers)
        {
            if (trigger.questName == questName)
            {
                trigger.QuestHandedIn();
                Debug.Log($"[Yarn] Called QuestHandedIn on: {questName}");
                return;
            }
        }

        Debug.LogWarning($"[Yarn] No QuestTrigger found for: {questName}");
    }


    public void TeleportHideAndSeek()
    {
        var potato = FindAnyObjectByType<HideAndSeek>();
        if (potato != null)
        {
            potato.ForceTeleport();
        }
        else
        {
            Debug.LogWarning("[Yarn] HideAndSeek component not found.");
        }
    }
}

