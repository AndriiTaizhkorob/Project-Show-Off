using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    public GameObject LoadingScreen;
    public Slider ProgressBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject );
        }
    }

    public void SwitchToScene(string nextSceneName)
    {
        LoadingScreen.SetActive(true);
        ProgressBar.value = 0;
        StartCoroutine(SwitchToSceneAsyc(nextSceneName));
    }

    IEnumerator SwitchToSceneAsyc(string nextSceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        while (!asyncLoad.isDone)
        {
            ProgressBar.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        LoadingScreen.SetActive(false);
    }
}
