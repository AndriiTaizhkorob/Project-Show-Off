using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuLoader : MonoBehaviour
{
    public InputActionReference startGameAction; // Assign "X" input here
    public string sceneToLoad = "Main Area";

    private bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && startGameAction.action.triggered)
        {
            hasStarted = true;
            StartGame();
        }
    }

    void StartGame()
    {
        if (LoadingScreenManager.Instance != null)
        {
            LoadingScreenManager.Instance.SwitchToScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("LoadingScreenManager not found! Falling back to basic load.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }
}
