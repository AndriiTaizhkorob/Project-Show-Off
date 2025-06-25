using System.Collections.Generic;
using UnityEngine;

public class PowerUIManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerIcon
    {
        public string questName;        // Must match EventTrigger in Quest
        public GameObject uiElement;    // Icon to show when active
    }

    [Header("Power Icons")]
    public List<PowerIcon> powerIcons;

    [Header("Dependencies")]
    [SerializeField] private QuestManager questManager;

    private Dictionary<string, GameObject> iconMap;

    private void Awake()
    {
        iconMap = new Dictionary<string, GameObject>();

        foreach (var power in powerIcons)
        {
            if (!string.IsNullOrEmpty(power.questName) && power.uiElement != null)
            {
                iconMap[power.questName] = power.uiElement;
                power.uiElement.SetActive(false); // Hide all icons initially
            }
        }
    }

    private void Update()
    {
        foreach (var entry in iconMap)
        {
            bool isActive = questManager.HasActiveQuest(entry.Key);
            entry.Value.SetActive(isActive);
        }
    }
}
