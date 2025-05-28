using UnityEngine;

public class FerrisWheelRotator : MonoBehaviour
{
    public Transform wheel;
    public float rotationSpeed = 10f;
    public Vector3 rotationAxis = Vector3.up;

    public Transform[] cartMounts;
    public Transform[] carts;

    public float swingAmplitude = 5f;
    public float swingSpeed = 2f;

    void Update()
    {
        wheel.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);

        for (int i = 0; i < carts.Length && i < cartMounts.Length; i++)
        {
            carts[i].position = cartMounts[i].position;

            Quaternion baseRotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);

            float swingAngle = Mathf.Sin(Time.time * swingSpeed + i) * swingAmplitude;
            Quaternion swingRotation = Quaternion.Euler(0f, 0f, swingAngle);

            carts[i].rotation = baseRotation * swingRotation;
        }
    }
}
