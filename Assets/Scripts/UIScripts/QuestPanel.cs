using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    [SerializeField]
    private QuestDisplay _questDisplayPrefab;
    [SerializeField]
    private RectTransform _questPrefab;
    [SerializeField]
    private GameObject _questSpawnPoint;
    [SerializeField]
    private Transform _questDisplayParent;
    [SerializeField]
    private float spacing;
    private Vector3 _questPosition;
    [SerializeField] private GameObject map;
    private int _questCount;

    private readonly List<QuestDisplay> _listDisplay = new();
    private List<GameObject> _listQuests = new();

    private void Awake()
    {
        if (map != null)
        {
            map.SetActive(false);
        }
    }

    void Start()
    {
        for (var i = _listDisplay.Count - 1; i >= 0; i--)
        {
            Destroy(_listDisplay[i].gameObject);
            _listDisplay.Remove(_listDisplay[i]);
        }

        _questPosition = _questSpawnPoint.transform.position;
        foreach (var quest in GameManager.Instance.QuestManager.Quests)
        {
            AddObjective(quest);
        }
        GameManager.Instance.QuestManager.OnQuestAdded += AddObjective;
    }

    private void Update()
    {
        if (map != null && map.activeInHierarchy)
        {
            foreach (QuestDisplay questLog in _listDisplay)
            {
                questLog.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (QuestDisplay questLog in _listDisplay)
            {
                questLog.gameObject.SetActive(true);
            }
        }
    }

    private void AddObjective(Quest quest)
    {
        var display = Instantiate(_questDisplayPrefab, _questDisplayParent);
        display.Init(quest);

        _listQuests.Add(display.gameObject);
        _listDisplay.Add(display);

        Debug.Log(quest.GetQuestName());
        display.GetComponent<QuestImageSpawner>().questName = quest.GetQuestName();
    }


    public void ResetCurrent(string questDescription)
    {
        Debug.Log(_listDisplay);

        for (var i = _listDisplay.Count - 1; i >= 0; i--)
        {
            if (TextCheck(_listDisplay[i].gameObject, questDescription))
            {
                Destroy(_listDisplay[i].gameObject);
                _listDisplay.RemoveAt(i);
                break;
            }
        }
    }


    private bool TextCheck(GameObject textObject, string textDescription)
    {
        return textObject.GetComponent<QuestDisplay>()._questText.GetComponent<TMP_Text>().text == textDescription;
    }
}
