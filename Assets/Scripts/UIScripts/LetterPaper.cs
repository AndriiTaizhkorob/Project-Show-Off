using System.Collections;
using UnityEngine;

public class LetterPaperAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, -300f, 0); 
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private GameObject typewriterObject; 

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private void OnEnable()
    {
        targetPosition = transform.localPosition;
        startPosition = targetPosition + offset;
        transform.localPosition = startPosition;

        StartCoroutine(AnimateLetterPaper());
    }

    [SerializeField] private TypewriterEffect typewriterEffect;

    private IEnumerator AnimateLetterPaper()
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;

        if (typewriterEffect != null)
            typewriterEffect.StartTyping(); 
    }

}
