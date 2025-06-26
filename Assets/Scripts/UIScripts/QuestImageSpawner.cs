using UnityEngine;
using UnityEngine.UI;

public class QuestImageSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] questImages;
    [SerializeField]
    private GameObject spawnLocation;

    [HideInInspector]
    public string questName;

    private void Start()
    {
        foreach (GameObject image in questImages)
        {
            if (image.name == questName)
            {
                Instantiate(image, spawnLocation.transform.position, Quaternion.identity, transform);
            }
        }
    }
}
