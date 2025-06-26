using UnityEngine;

public class BalloonPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private Transform balloonTarget;
    [SerializeField] private float showDistance = 2.5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || balloonTarget == null || promptUI == null)
            return;

        float distance = Vector3.Distance(player.position, balloonTarget.position);
        bool inRange = distance <= showDistance;

        if (inRange && !promptUI.activeSelf)
        {
            promptUI.SetActive(true);
        }
        else if (!inRange && promptUI.activeSelf)
        {
            promptUI.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }
}

