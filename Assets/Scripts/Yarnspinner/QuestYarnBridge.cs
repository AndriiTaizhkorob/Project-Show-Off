using UnityEngine;
using Yarn.Unity;

public class QuestYarnBridge : MonoBehaviour
{
    public QuestManager questManager;
    private DialogueRunner runner;

    private void Start()
    {
        runner = Object.FindFirstObjectByType<DialogueRunner>();
        if (runner != null)
        {
            runner.AddCommandHandler<string>("start_quest", StartQuestFromDialogue);
            runner.AddCommandHandler<string>("complete_quest", CompleteQuestFromDialogue);
            runner.AddCommandHandler("teleport_potato", TeleportHideAndSeek);
            runner.AddCommandHandler<string, string>("play_anim", PlayAnimTrigger);
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
    public void PlayAnimTrigger(string characterName, string triggerName)
    {
        var character = GameObject.Find(characterName);
        if (character == null)
            return;

        var animator = character.GetComponent<Animator>();
        if (animator == null)
            return;

        if (!HasTrigger(animator, triggerName))
            return;

        animator.SetTrigger(triggerName);
    }

    private bool HasTrigger(Animator animator, string triggerName)
    {
        foreach (var param in animator.parameters)
        {
            if (param.name == triggerName && param.type == AnimatorControllerParameterType.Trigger)
                return true;
        }
        return false;
    }

}

