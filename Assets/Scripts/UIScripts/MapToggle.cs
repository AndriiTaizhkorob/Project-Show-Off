using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class MapToggle : MonoBehaviour
{
    [SerializeField] private GameObject mapUI;
    [SerializeField] private InputActionReference toggleMapAction;
    [SerializeField] private MonoBehaviour playerMovementScript;

    [Header("UI Hiding")]
    [SerializeField] private GameObject hintUI; 
    [SerializeField] private GameObject tutorialManager;

    private DialogueRunner dialogueRunner;

    private void Awake()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    private void OnEnable()
    {
        toggleMapAction.action.performed += ToggleMap;
        toggleMapAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleMapAction.action.performed -= ToggleMap;
        toggleMapAction.action.Disable();
    }

    private void Update()
    {
        if (hintUI == null) return;

        bool mapIsOpen = mapUI != null && mapUI.activeSelf;
        bool dialogueIsActive = dialogueRunner != null && dialogueRunner.IsDialogueRunning;
        bool tutorialIsActive = tutorialManager != null && tutorialManager.activeSelf;

        hintUI.SetActive(!(mapIsOpen || dialogueIsActive || tutorialIsActive));
    }

    private void ToggleMap(InputAction.CallbackContext context)
    {
        if (mapUI == null)
            return;

        if (dialogueRunner != null && dialogueRunner.IsDialogueRunning)
            return;

        bool newState = !mapUI.activeSelf;
        mapUI.SetActive(newState);

        if (playerMovementScript != null)
            playerMovementScript.enabled = !newState;
    }
}


