using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PenguinGoalArea : MonoBehaviour
{
    public List<Transform> penguinSpots;
    private int currentSpotIndex = 0;

    public int rescuedPenguinCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        PenguinFollower penguin = other.GetComponent<PenguinFollower>();
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

        if (penguin != null && agent != null && currentSpotIndex < penguinSpots.Count)
        {
            penguin.SetReachedGoal();
            agent.SetDestination(penguinSpots[currentSpotIndex].position);

            rescuedPenguinCount++;
            currentSpotIndex++;
        }
    }
}
