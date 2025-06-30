using UnityEngine;

public class IntroLetterController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject letterUI;
    [SerializeField] private MonoBehaviour movementScript;
    [SerializeField] private string letterId = "intro_letter";
    [SerializeField] private CameraControls cameraControlScript;

    private bool letterSeen = false;

    [SerializeField] public string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    void Start()
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
        data.seenLetters.TryGetValue(letterId, out letterSeen);
    }

    public void SaveData(ref GameData data)
    {
        if (data.seenLetters.ContainsKey(letterId))
            data.seenLetters[letterId] = letterSeen;
        else
            data.seenLetters.Add(letterId, letterSeen);
    }
}

