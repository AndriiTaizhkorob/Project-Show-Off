using UnityEngine;
using System.Collections.Generic;

public class IceParticleCollision : MonoBehaviour
{
    public GameObject icePrefab;

    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numEvents = ParticlePhysicsExtensions.GetCollisionEvents(ps, other, collisionEvents);

        for (int i = 0; i < numEvents; i++)
        {
            Vector3 hitPos = collisionEvents[i].intersection;
            Instantiate(icePrefab, hitPos, Quaternion.identity);
        }
    }
}