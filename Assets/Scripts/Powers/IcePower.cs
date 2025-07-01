using FMOD.Studio;
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
    private EventInstance icePowerSound;

    void Awake()
    {
        iceBeam.Stop();
    }

    private void Start()
    {
        icePowerSound = AudioManager.instance.CreateInstance(FMODEvents.instance.icePower);
    }

    void Update()
    {
        if (shoot.action.triggered && powerOwner != null)
            PowerOn();

        if (isActive)
        {
            if (shoot.action.inProgress)
            {
                icePowerSound.setParameterByName("isLooping", 1);

                if (!cold.isPlaying)
                {
                    cold.Play();
                    iceBeam.Play();
                    icePowerSound.start();
                }
            }

            else
            {
                icePowerSound.setParameterByName("isLooping", 0);

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

    public void StopSound()
    {
        icePowerSound.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
