using UnityEngine;

public class FallInWater : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
            collision.gameObject.transform.position = spawnPoint.transform.position;
    }
}
