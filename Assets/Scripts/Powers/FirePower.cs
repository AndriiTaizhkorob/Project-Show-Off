using UnityEngine;
using UnityEngine.InputSystem;

public class FirePower : MonoBehaviour
{
    public InputActionReference shoot;

    [SerializeField]
    private float flameStregth = 1f;
    [SerializeField]
    private string searchedTag = "Flameable";
    [SerializeField]
    private GameObject[] flameableObjects;

    private Camera cam;

    [SerializeField]
    private GameObject currentObject;

    void Start()
    {
        cam = Camera.main;
        flameableObjects = GameObject.FindGameObjectsWithTag(searchedTag);
    }

    void Update()
    {
        if (shoot.action.triggered)
        {
            FindCurrentObject();
        }

        if (shoot.action.inProgress && currentObject != null)
        {
            FireUP();
        }
    }

    public void FindCurrentObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, transform.forward, out hit))
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
        if (Physics.Raycast(cam.transform.position, transform.forward, out hit))
        {
            if (hit.transform == currentObject.transform)
            {
                currentObject.GetComponent<HotAirBalloonRise>().Rise(flameStregth);
            }
        }
    }
}
