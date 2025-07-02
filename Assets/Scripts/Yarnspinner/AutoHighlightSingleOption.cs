using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FixSingleOptionSelectionIndicator : MonoBehaviour
{
    public string indicatorObjectName = "Selection Indicator";
    public Color activeColor = Color.white;
    public Color inactiveColor = new Color(1f, 1f, 1f, 0.5f);

    private GameObject firstSelectable = null;
    private bool optionsVisible = false;

    void OnEnable()
    {
        // Cache the first interactable option when the options become visible
        foreach (Transform option in transform)
        {
            var button = option.GetComponent<Button>();
            if (button != null && button.interactable && option.gameObject.activeInHierarchy)
            {
                firstSelectable = option.gameObject;
                EventSystem.current.SetSelectedGameObject(firstSelectable);
                optionsVisible = true;
                break;
            }
        }
    }

    void OnDisable()
    {
        firstSelectable = null;
        optionsVisible = false;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && optionsVisible)
        {
            if (EventSystem.current.currentSelectedGameObject == null && firstSelectable != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectable);
            }
        }
    }

    void Update()
    {
        // Fix for editor clicks breaking focus
        if (optionsVisible && EventSystem.current.currentSelectedGameObject == null && firstSelectable != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectable);
        }

        var selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null)
            return;

        foreach (Transform option in transform)
        {
            bool isSelected = option.gameObject == selected;

            // Selection indicator
            var indicator = option.Find(indicatorObjectName);
            if (indicator != null)
            {
                indicator.gameObject.SetActive(isSelected);
                var graphic = indicator.GetComponent<Graphic>();
                if (graphic != null)
                    graphic.color = isSelected ? activeColor : inactiveColor;
            }

            // Text is on the same GameObject
            var label = option.GetComponent<TextMeshProUGUI>();
            if (label != null)
                label.color = isSelected ? activeColor : inactiveColor;
        }
    }

    void LateUpdate()
    {
        // Force UI input refresh if the Game view is focused but EventSystem seems frozen
        if (optionsVisible && firstSelectable != null)
        {
            var es = EventSystem.current;
            if (es.currentSelectedGameObject == null || !es.currentSelectedGameObject.activeInHierarchy)
            {
                es.SetSelectedGameObject(firstSelectable);
            }

            // Force-select the selectable again even if it appears to be selected
            if (es.currentSelectedGameObject == firstSelectable)
            {
                es.SetSelectedGameObject(null);
                es.SetSelectedGameObject(firstSelectable);
            }
        }
    }
}