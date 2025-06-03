using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName; 
    public float reloadtime = 5f;
    private GameObject characterUI;
    private GameObject saveManager;

    private void Awake()
    {
        characterUI = GameObject.Find("characterUI");
        saveManager = GameObject.Find("DataPersistenceManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("LoadScene", reloadtime);
        }
    }

    void LoadScene()
    {
        //SceneManager.LoadScene(nextSceneName);
        characterUI.SetActive(true);
        DataPersistenceManager.Instance.SaveGame();
        LoadingScreenManager.Instance.SwitchToScene(nextSceneName);
    }
}