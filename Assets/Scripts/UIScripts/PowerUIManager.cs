using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUIManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerIcon
    {
        public string questName;
        public GameObject uiElement;
        public bool stayVisibleAfterQuestComplete = false;
    }


    [Header("Power Icons")]
    public List<PowerIcon> powerIcons;

    [Header("Dependencies")]
    [SerializeField] private QuestManager questManager;

    private Dictionary<string, GameObject> iconMap = new();
    private Dictionary<string, bool> iconState = new();

    private void Awake()
    {
        foreach (var power in powerIcons)
        {
            if (!string.IsNullOrEmpty(power.questName) && power.uiElement != null)
            {
                iconMap[power.questName] = power.uiElement;
                iconState[power.questName] = false;
                power.uiElement.SetActive(false);
            }
        }
    }
    private bool IsQuestComplete(string questName)
    {
        foreach (var quest in questManager.Quests)
        {
            if (quest.EventTrigger == questName && quest.IsComplete)
                return true;
        }
        return false;
    }

    private void Update()
    {
        foreach (var entry in iconMap)
        {
            string quest = entry.Key;
            GameObject icon = entry.Value;
            bool isCurrentlyVisible = iconState[quest];

            bool isActive = questManager.HasActiveQuest(quest);
            bool isComplete = IsQuestComplete(quest);

            PowerIcon iconData = powerIcons.Find(p => p.questName == quest);
            bool shouldBeVisible = isActive || (isComplete && iconData != null && iconData.stayVisibleAfterQuestComplete);

            if (shouldBeVisible && !isCurrentlyVisible)
            {
                StartCoroutine(AnimateIconIn(icon));
                iconState[quest] = true;
            }
            else if (!shouldBeVisible && isCurrentlyVisible)
            {
                icon.SetActive(false);
                iconState[quest] = false;
            }
        }

    }

    private IEnumerator AnimateIconIn(GameObject icon)
    {
        icon.transform.SetAsFirstSibling(); // put new icon at the top
        icon.SetActive(true);

        CanvasGroup group = icon.GetComponent<CanvasGroup>();
        if (group == null)
        {
            group = icon.AddComponent<CanvasGroup>();
        }

        group.alpha = 0f;
        icon.transform.localScale = Vector3.zero;

        float duration = 0.3f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            group.alpha = Mathf.Lerp(0f, 1f, t);
            icon.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            time += Time.deltaTime;
            yield return null;
        }

        group.alpha = 1f;
        icon.transform.localScale = Vector3.one;
    }
}


