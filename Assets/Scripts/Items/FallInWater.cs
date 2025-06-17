using UnityEngine;

public class FallInWater : MonoBehaviour
{
    [SerializeField]
    private GameObject iceCapsParent;
    [SerializeField]
    private GameObject Kitty;

    public GameObject spawnPoint;

    private float timer;

    public void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.position = spawnPoint.transform.position;

            if(!Kitty.GetComponent<QuestTrigger>().isAccepted)
                iceCapsParent.GetComponent<IsIcePathOn>().Reset();
        }
    }
}
