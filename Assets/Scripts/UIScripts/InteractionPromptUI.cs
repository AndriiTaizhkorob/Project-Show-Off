using UnityEngine;
using Yarn.Unity;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private Transform target;
    [SerializeField] private float showDistance = 3f;
    [SerializeField] private LayerMask playerMask;

    private Transform player;
    private DialogueRunner dialogueRunner;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        dialogueRunner = Object.FindFirstObjectByType<DialogueRunner>();

        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || target == null || promptUI == null || dialogueRunner == null)
            return;

        float distance = Vector3.Distance(player.position, target.position);
        bool inRange = distance <= showDistance;
        bool isDialogueRunning = dialogueRunner.IsDialogueRunning;

        if (inRange && !isDialogueRunning && !promptUI.activeSelf)
        {
            promptUI.SetActive(true);
        }
        else if ((!inRange || isDialogueRunning) && promptUI.activeSelf)
        {
            promptUI.SetActive(false);
        }
    }
}
