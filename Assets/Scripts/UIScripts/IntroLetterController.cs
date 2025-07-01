using UnityEngine;

public class IntroLetterController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject letterUI;
    [SerializeField] private MonoBehaviour movementScript;
    [SerializeField] private CameraControls cameraControlScript;

    public bool letterSeen = false;

    void Start()
    {
        LetterDisplay();
    }

    private void LetterDisplay()
    {
        if (!letterSeen)
        {
            ShowLetter();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowLetter()
    {
        if (letterUI != null)
            letterUI.SetActive(true);

        if (movementScript != null)
            movementScript.enabled = false;

        if (cameraControlScript != null)
            cameraControlScript.enabled = false;
    }

    public void CloseLetter()
    {
        if (letterUI != null)
            letterUI.SetActive(false);

        if (movementScript != null)
            movementScript.enabled = true;

        if (cameraControlScript != null)
            cameraControlScript.enabled = true;

        letterSeen = true;
        gameObject.SetActive(false); 
    }

    public void LoadData(GameData data)
    {
        letterSeen = data._letterSeen;
        LetterDisplay();
    }

    public void SaveData(ref GameData data)
    {
        data._letterSeen = letterSeen;
    }
}

