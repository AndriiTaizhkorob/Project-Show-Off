using FMODUnity;
using UnityEngine;

public class IceCapLife : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1f;

    private StudioEventEmitter emitter;

    private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        emitter.Play();
    }
}
