using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FixSingleOptionSelectionIndicator : MonoBehaviour
{
    public string indicatorObjectName = "Selection Indicator";
    public Color activeColor = Color.white;
    public Color inactiveColor = new Color(1f, 1f, 1f, 0.5f);

    void Update()
    {
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
}



