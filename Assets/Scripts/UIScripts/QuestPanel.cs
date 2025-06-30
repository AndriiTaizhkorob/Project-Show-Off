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
    public GameObject map;
    private int _questCount;

    private readonly List<QuestDisplay> _listDisplay = new();
    private List<GameObject> _listQuests = new();

    private void Awake()
    {
        map = GameObject.Find("Map");
        map.SetActive(false);
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
        if (map.activeInHierarchy)
        {
            foreach(QuestDisplay questLog in _listDisplay)
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

    private void AddObjective(Quest _quest)
    {
        for (var i = _listDisplay.Count - 1; i >= 0; i--)
        {
            var curObject = _listDisplay[i].transform.position;

            curObject.y -= _questPrefab.rect.height * 2 + spacing;
            _listDisplay[i].gameObject.transform.position = curObject;
        }

        var display = Instantiate(_questDisplayPrefab, _questPosition, Quaternion.identity, _questDisplayParent);
        _listQuests.Add(display.gameObject);
        display.Init(_quest);
        _listDisplay.Add(display);
        Debug.Log(_quest.GetQuestName());
        display.GetComponent<QuestImageSpawner>().questName = _quest.GetQuestName();
    }

    public void ResetCurrent(string questDescription)
    {
        Debug.Log(_listDisplay);

        for (var i = _listDisplay.Count - 1; i >= 0; i--)
        {
            var curObject = _listDisplay[i].transform.position;

            if (TextCheck(_listDisplay[i].gameObject, questDescription))
            {
                _questCount = i;

                Debug.Log("Correct");
                Destroy(_listDisplay[i].gameObject);
                _listDisplay.Remove(_listDisplay[i]);
            }

            if (i < _questCount)
            {
                curObject.y += _questPrefab.rect.height * 2 + spacing;

                Debug.Log("<Moved up>");
                Debug.Log(_listDisplay[i].transform.position);
                _listDisplay[i].gameObject.transform.position = curObject;
            }
        }
    }

    private bool TextCheck(GameObject textObject, string textDescription)
    {
       return textObject.GetComponent<QuestDisplay>()._questText.GetComponent<TMP_Text>().text == textDescription;
    }
}
