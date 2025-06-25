using FMOD.Studio;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
    [SerializeField]
    private int ambientNumber;
    private EventInstance localAmbient;

    void Awake()
    {
        switch(ambientNumber)
        {
            case 0:
                Debug.Log("Set the number between 1 and 4 for the ambient to play.");
                break;

            case 1:
                localAmbient = AudioManager.instance.CreateInstance(FMODEvents.instance.beachAmbient);
                break;

            case 2:
                localAmbient = AudioManager.instance.CreateInstance(FMODEvents.instance.forestAmbient);
                break;

            case 3:
                localAmbient = AudioManager.instance.CreateInstance(FMODEvents.instance.icelandAmbient);
                break;

            case 4:
                localAmbient = AudioManager.instance.CreateInstance(FMODEvents.instance.rainbowlandAmbient);
                break;
        }
    }

    void Start()
    {
        
    }
}
