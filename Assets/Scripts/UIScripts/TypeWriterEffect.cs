using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TypewriterEffect : MonoBehaviour
{
    [TextArea]
    public string fullText;

    public TextMeshProUGUI displayText;
    public float delay = 0.05f; 
    public float fastDelay = 0.005f; 

    public GameObject closeButton;
    public IntroLetterController letterController;

    public InputActionReference closeLetterAction;
    public InputActionReference speedUpAction; 

    private Coroutine typeCoroutine;
    private bool canClose = false;

    public void StartTyping()
    {
        if (displayText == null || string.IsNullOrEmpty(fullText))
            return;

        if (typeCoroutine != null)
            StopCoroutine(typeCoroutine);

        displayText.text = "";
        closeButton.SetActive(false);
        canClose = false;

        closeLetterAction?.action.Enable();
        speedUpAction?.action.Enable();

        typeCoroutine = StartCoroutine(TypeLine());
    }

    void OnDisable()
    {
        if (typeCoroutine != null)
            StopCoroutine(typeCoroutine);

        closeLetterAction?.action.Disable();
        speedUpAction?.action.Disable();
    }

    IEnumerator TypeLine()
    {
        foreach (char c in fullText)
        {
            displayText.text += c;

            bool speedingUp = speedUpAction != null && speedUpAction.action.IsPressed();
            yield return new WaitForSeconds(speedingUp ? fastDelay : delay);
        }

        closeButton.SetActive(true);
        canClose = true;
    }

    void Update()
    {
        if (canClose && closeLetterAction != null && closeLetterAction.action.triggered)
        {
            CloseLetter();
        }
    }

    void CloseLetter()
    {
        if (letterController != null)
        {
            letterController.CloseLetter();
        }
        else
        {
            closeButton?.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}



