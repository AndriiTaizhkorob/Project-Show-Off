using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    private GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 3)
        {
            player.GetComponent<Movement>().inWater = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 3)
        {
            player.GetComponent<Movement>().inWater = false;
        }
    }
}
