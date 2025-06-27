using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    public GameObject LoadingScreen;
    public Slider ProgressBar;
    public VideoPlayer loadingVideo;
    public RawImage videoImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SwitchToScene(string nextSceneName, float minLoadTime = 1.0f)
    {
        if (LoadingScreen != null)
            LoadingScreen.SetActive(true);

        if (ProgressBar != null)
            ProgressBar.value = 0;

        if (videoImage != null)
            videoImage.enabled = false;

        if (loadingVideo != null)
        {
            loadingVideo.Stop();
            loadingVideo.time = 0;
            loadingVideo.frame = 0;
            loadingVideo.isLooping = true;

            loadingVideo.prepareCompleted -= OnVideoPrepared; // Prevent multiple subscriptions
            loadingVideo.prepareCompleted += OnVideoPrepared;
            loadingVideo.Prepare();
        }

        StartCoroutine(SwitchToSceneAsync(nextSceneName, minLoadTime));
    }

    private void OnVideoPrepared(VideoPlayer source)
    {
        loadingVideo.Play();

        if (videoImage != null)
            videoImage.enabled = true;
    }

    IEnumerator SwitchToSceneAsync(string nextSceneName, float minLoadTime)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;

        float timer = 0f;

        while (timer < minLoadTime || asyncLoad.progress < 0.9f)
        {
            timer += Time.deltaTime;
            float loadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            float visualProgress = Mathf.Clamp01(timer / minLoadTime);

            if (ProgressBar != null)
                ProgressBar.value = Mathf.Min(loadProgress, visualProgress);

            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        // Wait one frame to allow scene activation to complete
        yield return null;

        if (LoadingScreen != null)
            LoadingScreen.SetActive(false);
    }
}

