using System.Collections.Generic;
using UnityEngine;

public class IsIcePathOn : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject kitty;

    private bool isOff = false;
    public List<GameObject> iceCaps;

    void Awake()
    {
        AddDescendantsWithTag(transform, "IceCap", iceCaps);
    }

    void Start()
    {
        if(isOff)
            gameObject.SetActive(false);
    }

    private void AddDescendantsWithTag(Transform parent, string tag, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                list.Add(child.gameObject);
            }
            AddDescendantsWithTag(child, tag, list);
        }
    }

    public void Reset()
    {
        foreach(GameObject child in iceCaps)
        {
            child.SetActive(true);
        }
    }

    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.monsterNames.Count; i++)
        {
            if (data.monsterNames[i] == kitty.name)
            {
                isOff = data.activeQuests[i];
            }
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}
