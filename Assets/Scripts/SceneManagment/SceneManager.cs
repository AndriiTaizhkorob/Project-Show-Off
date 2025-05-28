using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName; 
    public float reloadtime = 5f;
    public GameObject characterUI;

    private void Awake()
    {
        characterUI = GameObject.Find("characterUI");
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
        LoadingScreenManager.Instance.SwitchToScene(nextSceneName);
    }
}