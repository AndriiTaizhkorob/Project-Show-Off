using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestoreUIFocus : MonoBehaviour
{
    private GameObject firstOptionButton;
    private bool optionsVisible = false;

    void OnEnable()
    {
        // Look for the first active selectable button in children
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (var button in buttons)
        {
            if (button.interactable && button.gameObject.activeInHierarchy)
            {
                firstOptionButton = button.gameObject;
                EventSystem.current.SetSelectedGameObject(firstOptionButton);
                optionsVisible = true;
                break;
            }
        }
    }

    void OnDisable()
    {
        optionsVisible = false;
        firstOptionButton = null;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && optionsVisible)
        {
            if (EventSystem.current.currentSelectedGameObject == null && firstOptionButton != null)
            {
                EventSystem.current.SetSelectedGameObject(firstOptionButton);
            }
        }
    }
}
