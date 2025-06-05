using UnityEngine;
using UnityEngine.InputSystem;

public class FirePower : MonoBehaviour
{
    [Header("Fire power button")]
    [SerializeField]
    private InputActionReference shoot;

    [Header("Flame parameters")]
    [SerializeField]
    private ParticleSystem flame;
    [SerializeField]
    private float flameStregth = 1f;

    [Header("Tag of objects")]
    [SerializeField]
    private string searchedTag = "Flameable";

    private GameObject[] flameableObjects;
    private Camera cam;
    private GameObject currentObject;

    void Start()
    {
        cam = Camera.main;
        flameableObjects = GameObject.FindGameObjectsWithTag(searchedTag);
    }

    void Update()
    {
        if (shoot.action.triggered)
            FindCurrentObject();

        if (shoot.action.inProgress && currentObject != null)
            FireUP();

        if (shoot.action.inProgress)
            flame.Play();

        else
            flame.Stop();
    }

    public void FindCurrentObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            foreach (GameObject i in flameableObjects)
            {
                if (hit.transform == i.transform)
                {
                    currentObject = i; 
                    break;
                }
            }
        }
    }

    public void FireUP()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, flame.main.duration))
        {
            if (hit.transform == currentObject.transform)
            {
                currentObject.GetComponent<HotAirBalloonRise>().Rise(flameStregth);
            }
        }
    }
}
