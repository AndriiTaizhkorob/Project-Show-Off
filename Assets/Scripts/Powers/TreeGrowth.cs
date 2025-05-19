using UnityEngine;
using UnityEngine.InputSystem;

public class TreeGrowth : MonoBehaviour
{
    public InputActionReference interaction;

    [SerializeField] 
    private ParticleSystem firework;
    [SerializeField]
    private float growthSpeed;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private int pickUpValue;
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private float scaleLimit;
    [SerializeField]
    private GameObject[] treePrefabs;

    private GameObject treeObj;
    private bool isSpawned = false;
    private bool isPlayed = false;

    void Update()
    {
        if (gameObject.GetComponent<ProgressManager>().enabled && interaction.action.triggered && !isSpawned && Physics.CheckSphere(transform.position, checkRadius, playerMask))
            Spawn();

        if (gameObject.GetComponent<ProgressManager>().enabled && interaction.action.inProgress && Physics.CheckSphere(transform.position, checkRadius, playerMask) && treeObj.transform.localScale.x < scaleLimit)
            treeObj.transform.localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed) * Time.deltaTime;

        if (isSpawned && treeObj.transform.localScale.x >= scaleLimit && !isPlayed)
            Play();
    }

    private void Spawn()
    {
        treeObj = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], gameObject.transform.position, Quaternion.identity);
        isSpawned = true;
    }

    private void Play()
    {
        firework.Play();
        isPlayed = true;
        gameObject.GetComponent<ProgressManager>().UpdateScore(pickUpValue);
    }
}
