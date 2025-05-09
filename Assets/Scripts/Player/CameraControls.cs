using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public InputActionReference look;

    public float mouseSensitivity = 100.0f;

    public Transform playerBody;

    float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float horizontal = look.action.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float vertical = look.action.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.transform.Rotate(Vector3.up * horizontal);
    }
}
