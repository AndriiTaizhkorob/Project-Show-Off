using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Player")]
    [field: SerializeField] public EventReference footSteps { get; private set; }
    [field: SerializeField] public EventReference jump { get; private set; }
    [field: SerializeField] public EventReference landing { get; private set; }


    [field: Header("Environment")]
    [field: SerializeField] public EventReference forestAmbient { get; private set; }
    [field: SerializeField] public EventReference icelandAmbient { get; private set; }
    [field: SerializeField] public EventReference rainbowlandAmbient { get; private set; }
    [field: SerializeField] public EventReference beachAmbient { get; private set; }
    [field: SerializeField] public EventReference ferrisWheel { get; private set; }
    [field: SerializeField] public EventReference hotAirBalloon { get; private set; }
    [field: SerializeField] public EventReference iceCracking { get; private set; }
    [field: SerializeField] public EventReference waterSplash { get; private set; }
    [field: SerializeField] public EventReference penguinSounds { get; private set; }


    [field: Header("Powers")]
    [field: SerializeField] public EventReference firePower { get; private set; }
    [field: SerializeField] public EventReference icePower { get; private set; }
    [field: SerializeField] public EventReference treeGrowthPower { get; private set; }
    [field: SerializeField] public EventReference teleportationPower { get; private set; }


    [field: Header("System & UI")]
    [field: SerializeField] public EventReference crossOut { get; private set; }
    [field: SerializeField] public EventReference interact { get; private set; }
    [field: SerializeField] public EventReference succeess { get; private set; }

    [field: Header("Voice Lines")]
    [field: SerializeField] public EventReference Potato1 { get; private set; }


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
