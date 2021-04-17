using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_CameraController : MonoBehaviour
{
    public float rotationSensitivity;
    public Vector2 cameraInput;
    public Vector2 maxMinRotation;

    public Vector3 cameraPosition_NOaim;

    public Transform playerTarget;
    public Camera playerCamera;


    private void Update()
    {    
        RotateCamera();   
    }

    void RotateCamera() 
    {
        cameraInput.y += Input.GetAxis("Mouse X") * rotationSensitivity;
        cameraInput.x += -Input.GetAxis("Mouse Y") * rotationSensitivity;
        cameraInput.x = Mathf.Clamp(cameraInput.x, -maxMinRotation.x, maxMinRotation.y);
    }

    private void LateUpdate()
    {
        Vector3 dir = cameraPosition_NOaim;

        Quaternion rotation = Quaternion.Euler(cameraInput.x, cameraInput.y, 0);
        rotation.x = Mathf.Clamp(rotation.x, -maxMinRotation.x, maxMinRotation.y);
        transform.position = playerTarget.transform.position + rotation * dir;
        transform.LookAt(playerTarget.transform.position);
    }
}
