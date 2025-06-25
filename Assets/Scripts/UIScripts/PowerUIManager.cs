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

    private void Update()
    {
        foreach (var entry in iconMap)
        {
            bool shouldBeVisible = questManager.HasActiveQuest(entry.Key);
            bool isCurrentlyVisible = iconState[entry.Key];

            if (shouldBeVisible && !isCurrentlyVisible)
            {
                StartCoroutine(AnimateIconIn(entry.Value));
                iconState[entry.Key] = true;
            }
            else if (!shouldBeVisible && isCurrentlyVisible)
            {
                entry.Value.SetActive(false);
                iconState[entry.Key] = false;
            }
        }
    }

    private IEnumerator AnimateIconIn(GameObject icon)
    {
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

