using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput; // Reference to PlayerInput component

    public string gameplayMap = "Gameplay";
    public string uiMap = "UI";

    public void EnableGameplay()
    {
        playerInput.SwitchCurrentActionMap(gameplayMap);
        Debug.Log("Gameplay input enabled.");
    }

    public void EnableUI()
    {
        playerInput.SwitchCurrentActionMap(uiMap);
        Debug.Log("UI input enabled.");
    }
}