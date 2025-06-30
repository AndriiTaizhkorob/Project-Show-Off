using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class TutorialStep
{
    public string message;
    public InputActionReference inputAction;
    public Texture buttonIcon;
    public float extraDelayAfterClick = 0f;
    public bool waitUntilCondition = false;
    public bool showPowerHint = false;
    public string powerHintMessage = "";
}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private float initialDelay = 3f;

    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private RawImage iconImage;
    [SerializeField] private TextMeshProUGUI rightText;

    [SerializeField] private List<TutorialStep> steps = new();
    [SerializeField] private float delayBetweenSteps = 2f;

    [SerializeField] private GameObject tutorialUIRoot;
    [SerializeField] private GameObject mapUI;

    [SerializeField] private GameObject powerTutorial;
    [SerializeField] private List<RawImage> powerIcons = new();
    [SerializeField] private float iconPopDelay = 0.3f;
    [SerializeField] private TextMeshProUGUI powerHintText;

    [SerializeField] private GameObject introLetterUI;

    private int currentStep = 0;

    private static bool tutorialCompletedThisSession = false;

    private void Start()
    {
        if (tutorialCompletedThisSession)
        {
            tutorialUIRoot.SetActive(false);
            enabled = false;
            return;
        }

        tutorialUIRoot.SetActive(false); // Hide UI before initial delay
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        while (introLetterUI != null && introLetterUI.activeSelf)
            yield return null;

        yield return new WaitForSeconds(initialDelay);
        tutorialUIRoot.SetActive(true);
        StartCoroutine(RunTutorial());
        StartCoroutine(PulseIcon());
    }



    IEnumerator RunTutorial()
    {
        while (currentStep < steps.Count)
        {
            var step = steps[currentStep];
            ShowStep(step);

            bool inputPerformed = false;
            System.Action<InputAction.CallbackContext> onPerformed = ctx => inputPerformed = true;
            step.inputAction.action.performed += onPerformed;

            while (!inputPerformed)
                yield return null;

            step.inputAction.action.performed -= onPerformed;

            if (step.waitUntilCondition)
            {
                SetRowVisible(false); // hide UI immediately
                yield return new WaitUntil(() => IsMapClosed());
                yield return new WaitForSeconds(delayBetweenSteps);
            }
            else
            {
                yield return new WaitForSeconds(step.extraDelayAfterClick); // let text linger
                SetRowVisible(false);
                yield return new WaitForSeconds(delayBetweenSteps);
            }

            currentStep++;
        }

        tutorialCompletedThisSession = true;
        tutorialUIRoot.SetActive(false);
        enabled = false;
        gameObject.SetActive(false);
    }

    private void ShowStep(TutorialStep step)
    {
        string[] parts = step.message.Split(new[] { "[icon]" }, System.StringSplitOptions.None);

        leftText.text = parts.Length > 0 ? parts[0] : "";
        rightText.text = parts.Length > 1 ? parts[1] : "";
        iconImage.texture = step.buttonIcon;
        iconImage.enabled = step.buttonIcon != null;

        if (step.showPowerHint)
        {
            powerTutorial.SetActive(true);
            powerHintText.text = step.powerHintMessage;
            powerHintText.enabled = true;

            foreach (var icon in powerIcons)
            {
                var c = icon.color;
                c.a = 0;
                icon.color = c;
            }
            StopCoroutine(nameof(PopUpPowerIcons));
            StartCoroutine(PopUpPowerIcons());
        }
        else
        {
            powerTutorial.SetActive(false);
            powerHintText.enabled = false;
        }

        SetRowVisible(true);
    }


    private void SetRowVisible(bool visible)
    {
        leftText.enabled = visible;
        rightText.enabled = visible;
        iconImage.enabled = visible;
        powerTutorial.SetActive(visible && steps[currentStep].showPowerHint);
        if (!powerTutorial.activeSelf)
        {
            foreach (var icon in powerIcons)
            {
                var c = icon.color;
                c.a = 0;
                icon.color = c;
            }
        }
    }


    IEnumerator PulseIcon()
    {
        Vector3 originalScale = iconImage.transform.localScale;
        float pulseSpeed = 2f;
        float pulseAmount = 0.15f;

        while (true)
        {
            float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            iconImage.transform.localScale = originalScale * scale;
            yield return null;
        }
    }

    private bool IsMapClosed()
    {
        return mapUI != null && !mapUI.activeSelf;
    }
    IEnumerator PopUpPowerIcons()
    {
        for (int i = 0; i < powerIcons.Count; i++)
        {
            yield return StartCoroutine(FadeInIcon(powerIcons[i]));
            yield return new WaitForSeconds(iconPopDelay);
        }
    }

    IEnumerator FadeInIcon(RawImage icon)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color color = icon.color;
        color.a = 0;
        icon.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            icon.color = color;
            yield return null;
        }

        color.a = 1;
        icon.color = color;
    }

}


