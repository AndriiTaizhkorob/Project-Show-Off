using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Dragon SFX")]


    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference archerWizardFootsteps { get; private set; }


    [field: Header("UI and Other SFX")]

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("More than one Audio Manager in the scene.");
        }
        instance = this;
    }
}
