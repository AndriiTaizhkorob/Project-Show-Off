using UnityEngine;
using Yarn.Unity;
using FMODUnity;
using FMOD.Studio;

public class YarnAudioCommands : MonoBehaviour
{

    private EventInstance _instance;

    public void Awake()
    {
        // Register the command
        var dialogueRunner = GameObject.Find("Dialogue System Variant").GetComponent<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string>("playvoice", PlayVoice);
        dialogueRunner.AddCommandHandler("stopvoice", StopVoice);
    }

    private void PlayVoice(string eventPath)
    {
        // Play the FMOD event
        StopVoice();
        _instance = FMODUnity.RuntimeManager.CreateInstance("event:/VoiceLines/" + eventPath);
        _instance.start();
    }
    private void StopVoice()
    {
        // Stop the FMOD event
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}