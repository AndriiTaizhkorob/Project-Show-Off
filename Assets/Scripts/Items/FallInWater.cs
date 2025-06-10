using UnityEngine;

public class FallInWater : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
            collision.gameObject.transform.position = spawnPoint.transform.position;
    }
}
