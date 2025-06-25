using FMODUnity;
using UnityEngine;

public class IceCapScripted : MonoBehaviour
{
    public float lifeTime;

    private bool isTouched = false;
    private float timer = 0f;

    private StudioEventEmitter emitter;

    private void Awake()
    {
        timer = lifeTime;
        emitter = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {

        if (isTouched)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                gameObject.SetActive(false);
                isTouched = false;
                timer = lifeTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isTouched = true;
    }

    private void OnDisable()
    {
        emitter.Play();
    }
}
