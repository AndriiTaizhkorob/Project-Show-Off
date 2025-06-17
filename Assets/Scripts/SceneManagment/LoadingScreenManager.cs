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

    public void SwitchToScene(string nextSceneName, float minLoadTime = 1.0f)
    {
        LoadingScreen.SetActive(true);
        ProgressBar.value = 0;
        StartCoroutine(SwitchToSceneAsync(nextSceneName, minLoadTime));
    }


    IEnumerator SwitchToSceneAsync(string nextSceneName, float minLoadTime)
    {
        float timer = 0f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            timer += Time.deltaTime;
            ProgressBar.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Wait until both loading is done and the minimum time has passed
            if (asyncLoad.progress >= 0.9f && timer >= minLoadTime)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        LoadingScreen.SetActive(false);
    }

}
