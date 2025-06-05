using UnityEngine;

public class IceCapLife : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
            Destroy(gameObject);
    }
}
