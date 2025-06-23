using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class IcePower : MonoBehaviour
{
    [SerializeField]
    private InputActionReference shoot;

    [SerializeField]
    private ParticleSystem cold;
    [SerializeField]
    private VisualEffect iceBeam;
    [SerializeField]
    private GameObject powerOwner;

    private bool isActive;

    void Awake()
    {
        iceBeam.Stop();
    }

    void Update()
    {
        if (shoot.action.triggered && powerOwner != null)
            PowerOn();

        if (isActive)
        {
            if (shoot.action.inProgress)
            {
                if (!cold.isPlaying)
                {
                    cold.Play();
                    iceBeam.Play();
                }
            }

            else
            {
                if (cold.isPlaying)
                {
                    cold.Stop();
                    iceBeam.Stop();
                }
            }
        }
    }
    
    public void PowerOn()
    {
        isActive = powerOwner.GetComponent<QuestTrigger>().isAccepted;
    }
}
