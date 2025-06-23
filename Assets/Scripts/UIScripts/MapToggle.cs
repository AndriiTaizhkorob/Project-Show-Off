using UnityEngine;
using UnityEngine.InputSystem;

public class MapToggle : MonoBehaviour
{
    [SerializeField] private GameObject mapUI;
    [SerializeField] private InputActionReference toggleMapAction;
    [SerializeField] private MonoBehaviour playerMovementScript; // Your movement script (e.g. PlayerMovement)

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

    private void ToggleMap(InputAction.CallbackContext context)
    {
        if (mapUI == null) return;

        bool newState = !mapUI.activeSelf;
        mapUI.SetActive(newState);

        if (playerMovementScript != null)
            playerMovementScript.enabled = !newState; // Disable movement if map is now open

    }
}

