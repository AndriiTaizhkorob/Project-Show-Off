using UnityEngine;

public class MainAreaDecoration : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject[] decorationObjects;

    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.monsterNames.Count; i++)
        {
            for(int j = 0; j < decorationObjects.Length; j++){            
                if (data.finishedQuests[i] && data.monsterNames[i] == decorationObjects[j].name)
                {
                    decorationObjects[j].SetActive(true);
                    Debug.Log("Scored");
                }
                Debug.Log(data.finishedQuests[i]);
                Debug.Log(data.monsterNames[i]);
                Debug.Log(decorationObjects[j].name);
            }
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}
