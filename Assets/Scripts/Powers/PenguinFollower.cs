using UnityEngine;
using UnityEngine.AI;

public class PenguinFollower : MonoBehaviour, IDataPersistence
{
    [Header("Follow Settings")]
    public float followRange = 5f;
    public float stopDistance = 2f;
    public Transform followTarget;

    [Header("Wander Settings")]
    public float wanderRadius = 5f;
    public float wanderInterval = 4f;

    [Header("Facing Settings")]
    public float facePlayerRadius = 6f; 

    private bool isFollowing = false;
    private bool hasReachedGoal = false;
    private float wanderTimer;

    private NavMeshAgent agent;
    private Vector3 startPos;
    private Vector3 localPosition;
    private bool hasScored;
    private GameObject npc;

    [HideInInspector]
    public bool scored = false;

    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        wanderTimer = wanderInterval;

    }

    void Start()
    {
        WanderNow(); // Begin wandering immediately
        npc = GetComponent<ProgressManager>().npc;
    }

    void Update()
    {

        if (followTarget == null) return;

        float distance = Vector3.Distance(transform.position, followTarget.position);

        if (hasReachedGoal)
        {
            if (distance <= facePlayerRadius)
            {
                // Smoothly rotate to face the player
                Vector3 dir = followTarget.position - transform.position;
                dir.y = 0;
                if (dir != Vector3.zero)
                {
                    Quaternion look = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2f);
                }
            }
            return;
        }

        if(distance < followRange)
            npc = GetComponent<ProgressManager>().npc;

        if (distance < followRange && npc != null)
        {
            isFollowing = true;
            agent.SetDestination(followTarget.position);

            // Smoothly rotate while following
            Vector3 dir = followTarget.position - transform.position;
            dir.y = 0;
            if (dir != Vector3.zero)
            {
                Quaternion look = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 5f);
            }
        }
        else if (distance > followRange * 1.2f)
        {
            if (isFollowing)
                agent.ResetPath();

            isFollowing = false;
        }

        if (!isFollowing)
        {
            wanderTimer += Time.deltaTime;
            if (wanderTimer >= wanderInterval)
            {
                WanderNow();
            }
        }
    }

    public void SetReachedGoal()
    {
        isFollowing = false;
        hasReachedGoal = true;
        scored = true;
        agent.ResetPath();
    }

    private void WanderNow()
    {
        Vector3 newPos = RandomNavSphere(startPos, wanderRadius, -1);
        agent.SetDestination(newPos);
        wanderTimer = 0;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask))
        {
            return navHit.position;
        }

        return origin;
    }

    public void LoadData(GameData data)
    {
        data.penguinPosition.TryGetValue(id, out localPosition);
        data.penguinScored.TryGetValue(id, out hasScored);

        if(localPosition != new Vector3(0, 0, 0))
            gameObject.transform.position = localPosition;

        scored = hasScored;
    }

    public void SaveData(ref GameData data)
    {
        if (data.penguinPosition.ContainsKey(id))
        {
            data.penguinPosition.Remove(id);
        }
        localPosition = gameObject.transform.position;

        data.penguinPosition.Add(id, localPosition);

        if (data.penguinScored.ContainsKey(id))
        {
            data.penguinScored.Remove(id);
        }
        hasScored = scored;

        data.penguinScored.Add(id, hasScored);
    }
}

