using System.Collections.Generic;
using TMPro;
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

    private Vector3 _questPosition;

    private readonly List<QuestDisplay> _listDisplay = new();

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

    private void AddObjective(Quest _quest)
    {
        var display = Instantiate(_questDisplayPrefab, _questPosition, Quaternion.identity, _questDisplayParent);
        display.Init(_quest);
        _listDisplay.Add(display);
        _questPosition = new Vector3(_questPosition.x, _questPosition.y - _questPrefab.rect.height * 2, _questPosition.z);
    }

    public void ResetCurrent(string questDescription)
    {
        Debug.Log(_listDisplay);

        for (var i = _listDisplay.Count - 1; i >= 0; i--)
        {
            if (_listDisplay[i].gameObject.GetComponent<TMP_Text>().text == questDescription)
            {
                Debug.Log("Correct");
                Destroy(_listDisplay[i].gameObject);
                _listDisplay.Remove(_listDisplay[i]);
            }
        }

        _questPosition = _questSpawnPoint.transform.position;
    }
}
