using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Threading;

public class CharacterFaceView : DialoguePresenterBase
{
    [System.Serializable]
    public class CharacterFace
    {
        public string characterName;
        public GameObject faceUI;
    }

    [Header("Faces")]
    public List<CharacterFace> characterFaces;

    private Dictionary<string, GameObject> faceMap;

    private void Awake()
    {
        faceMap = new Dictionary<string, GameObject>();

        foreach (var entry in characterFaces)
        {
            if (!string.IsNullOrEmpty(entry.characterName) && entry.faceUI != null)
            {
                faceMap[entry.characterName.ToLower()] = entry.faceUI;
                entry.faceUI.SetActive(false);
            }
        }
    }

    public override YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken cancellationToken)
    {
        string speaker = line.CharacterName?.ToLower();

        foreach (var kvp in faceMap)
        {
            kvp.Value.SetActive(kvp.Key == speaker);
        }

        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueStartedAsync()
    {
        return YarnTask.CompletedTask;
    }


    public override YarnTask OnDialogueCompleteAsync()
    {
        foreach (var face in faceMap.Values)
        {
            face.SetActive(false);
        }

        return YarnTask.CompletedTask;
    }
    public override YarnTask<DialogueOption?> RunOptionsAsync(DialogueOption[] dialogueOptions, CancellationToken cancellationToken)
    {
        return DialogueRunner.NoOptionSelected;
    }
}
