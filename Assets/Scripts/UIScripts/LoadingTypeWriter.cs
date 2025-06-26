using System.Collections;
using UnityEngine;
using TMPro;

public class LoadingTypewriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private string fullText = "Loading...";
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private float loopDelay = 0.75f;

    private void OnEnable()
    {
        StartCoroutine(TypeLoop());
    }

    IEnumerator TypeLoop()
    {
        while (true)
        {
            loadingText.text = "";

            for (int i = 0; i <= fullText.Length; i++)
            {
                loadingText.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(loopDelay);
        }
    }
}
