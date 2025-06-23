
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.EventSystems;
using System.Collections;

public class HideAndSeek : MonoBehaviour, IDataPersistence
{
    private GameObject[] tpSpots;
    private GameObject questUI;
    public int currentValue;
    public int spotNumber;
    private int spotLimit;
    private bool inProgress;
    public VisualEffect teleportEffect;

    void Awake()
    {
        teleportEffect.Stop();
    }

    void Start()
    {
        tpSpots = GetComponent<QuestTrigger>().Objects;
        questUI = GetComponent<QuestTrigger>().characterUI;
        spotLimit = GetComponent<QuestTrigger>().itemAmount;
    }

    void Update()
    {
        inProgress = GetComponent<QuestTrigger>().isAccepted;
        currentValue = GetComponent<QuestTrigger>().currentValue;
        var runner = FindAnyObjectByType<Yarn.Unity.DialogueRunner>();

        if (runner != null)
        {
            runner.VariableStorage.SetValue("$potato_progress", spotNumber);
        }
    }
    public void ForceTeleport()
    {
        if (inProgress && currentValue == spotNumber && currentValue != spotLimit)
        {
            teleportEffect.Play();

            StartCoroutine(DelayActivation());
        }
    }

    IEnumerator DelayActivation()
    {
        yield return new WaitForSeconds(1f);
        transform.position = tpSpots[currentValue].transform.position;
        spotNumber++;
        teleportEffect.Stop();
    }

    public void LoadData(GameData data)
    {
        spotNumber = (data.currentSpot > currentValue) ? data.currentSpot - 1 : data.currentSpot;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSpot = spotNumber;
    }
}
