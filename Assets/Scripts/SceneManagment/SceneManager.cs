using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName; 
    public float reloadtime = 5f; 

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
        LoadingScreenManager.Instance.SwitchToScene(nextSceneName);
    }
}