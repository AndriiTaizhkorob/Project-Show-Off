using UnityEngine;

public class IceSurface : MonoBehaviour
{
    public GameObject frozenOverlay;

    public void FreezeWater(Vector3 position)
    {
        Instantiate(frozenOverlay, position, Quaternion.identity);
    }
}
