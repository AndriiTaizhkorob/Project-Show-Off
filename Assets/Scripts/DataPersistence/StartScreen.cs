using UnityEngine;

public class StartScreen : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject dataHandler;

    private void Awake()
    {
        dataHandler = GameObject.Find("DataPersistenceManager");
    }

    void Start()
    {
        dataHandler.GetComponent<DataPersistenceManager>().DeleteSave();
    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {

    }
}
