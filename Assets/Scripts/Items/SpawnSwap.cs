using UnityEngine;

public class SpawnSwap : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPointer;
    [SerializeField]
    private GameObject spawnOwner;

    private void OnTriggerEnter(Collider other)
    {
        spawnOwner.GetComponent<FallInWater>().spawnPoint = spawnPointer;
    }
}
