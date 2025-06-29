using UnityEngine;

public class SimpleAnimationSetter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName = "Dance";

    [Tooltip("Set this to true in Inspector or via other means to start the animation")]
    public bool activateDance = false;

    private bool hasTriggered = false;

    void Update()
    {
        if (activateDance && !hasTriggered)
        {
            if (animator != null)
            {
                animator.SetTrigger(triggerName);
                hasTriggered = true;
                Debug.Log("Dance triggered!");
            }
            else
            {
                Debug.LogWarning("Animator not assigned!");
            }
        }
    }
}
