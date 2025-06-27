using UnityEngine;

public class MainAreaDecoration : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject[] decorationObjects;

    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.monsterNames.Count; i++)
        {
            if (data.finishedQuests[i] && data.monsterNames[i] == decorationObjects[i].name)
            {
                decorationObjects[i].SetActive(true);
            }
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}
