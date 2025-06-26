using UnityEngine;

public class BalloonBobbing : MonoBehaviour
{
    public float bobAmplitude = 0.25f;
    public float bobSpeed = 1.5f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * bobSpeed) * bobAmplitude;
        transform.localPosition = initialPosition + new Vector3(0, offset, 0);
    }
}

