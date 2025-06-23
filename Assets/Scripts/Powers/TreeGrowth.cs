using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class TreeGrowth : MonoBehaviour, IDataPersistence
{
    public InputActionReference interaction;

    [SerializeField] 
    private ParticleSystem firework;
    [SerializeField] 
    private VisualEffect growth;
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
    private bool isPlaying = false;
    private float localSize = 0f;

    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Awake()
    {
        growth.Stop();
    }

    void Update()
    {
        if (gameObject.GetComponent<ProgressManager>().enabled && interaction.action.triggered && !isSpawned && Physics.CheckSphere(transform.position, checkRadius, playerMask))
            Spawn();

        if (gameObject.GetComponent<ProgressManager>().enabled && interaction.action.inProgress && Physics.CheckSphere(transform.position, checkRadius, playerMask) && treeObj.transform.localScale.x < scaleLimit)
        {
            treeObj.transform.localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed) * Time.deltaTime;
            if (!isPlaying)
            {
                growth.Play();
                isPlaying = true;
            }

        }
        else
        {
            if (isPlaying)
            {
                growth.Stop();
                isPlaying = false;
            }
        }


        if (isSpawned && treeObj.transform.localScale.x >= scaleLimit && !isPlayed)
            Play(); 

        if(isSpawned)
            localSize = treeObj.transform.localScale.x;
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

    public void LoadData(GameData data)
    {
        data.growthProgress.TryGetValue(id, out localSize);
        if(localSize > 0)
        {
            if (localSize >= scaleLimit)
            {
                isPlayed = true;
            }

            Spawn();
            treeObj.transform.localScale = new Vector3(localSize, localSize, localSize);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.growthProgress.ContainsKey(id))
        {
            data.growthProgress.Remove(id);
        }
        data.growthProgress.Add(id, localSize);
    }
}
