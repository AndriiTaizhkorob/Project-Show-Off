using UnityEngine;
using UnityEngine.InputSystem;

public class IcePower : MonoBehaviour
{
    [SerializeField]
    private InputActionReference shoot;

    [SerializeField]
    private ParticleSystem cold;

    //[SerializeField]
    //private string searchedTag = "Water";

    //[SerializeField]
    //private LayerMask layerMask;

    //private GameObject[] waterBodies;
    //private GameObject currentObject;
    //private Camera cam;

    void Awake()
    {
        //cam = Camera.main;
        //waterBodies = GameObject.FindGameObjectsWithTag(searchedTag);
    }

    void Update()
    {
        //if (shoot.action.triggered)
            //FindCurrentObject();

        //if (shoot.action.inProgress && currentObject != null)
            //Freeze();

        if (shoot.action.inProgress)
            cold.Play();

        else
            cold.Stop();
    }

    //Legacy code.
    //public void FindCurrentObject()
    //{
        //RaycastHit hit;
        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        //{
            //Debug.Log(hit.transform);
            //foreach (GameObject i in waterBodies)
            //{
                //if (hit.transform == i.transform)
                //{
                    //currentObject = i;
                    //break;
                //}
            //}
        //}
    //}

    //Legacy code.
    //public void Freeze()
    //{
        //RaycastHit hit;
        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, layerMask))
        //{
            //if (hit.transform == currentObject.transform)
            //{
                //currentObject.GetComponent<IceSurface>().FreezeWater(hit.point);
            //}
        //}
    //}
}
