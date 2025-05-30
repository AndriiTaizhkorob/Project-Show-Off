using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using static UnityEditor.SceneView;

public class CameraControls : MonoBehaviour
{
    public InputActionReference look;
    public InputActionReference aim;

    public float mouseSensitivity = 100.0f;

    public Transform playerBody;
    public GameObject characterUI;

    public bool activeControls = true;

    float xRotation = 0f;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterUI = GameObject.Find("characterUI");
    }


    void Update()
    {
        if (!aim.action.inProgress && characterUI.activeInHierarchy)
        {
            activeControls = false;
        }
        else
        {
            activeControls = true;
        }

        if (activeControls)
        {
            Cursor.lockState = CursorLockMode.Locked;
            CameraInput();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CameraInput()
    {
        float horizontal = look.action.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float vertical = look.action.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -25f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.transform.Rotate(Vector3.up * horizontal);
    }
}
