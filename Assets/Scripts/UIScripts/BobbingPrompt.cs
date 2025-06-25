using UnityEngine;

public class BobbingPrompt : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float scaleSpeed = 4f;           // How fast it bobs
    public float scaleAmount = 0.05f;       // How much it enlarges/shrinks
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = originalScale + Vector3.one * scaleOffset;
    }
}
