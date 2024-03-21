using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationHandler : CameraFeature
{
    public float rotateSpeed = 5f; 

    private Vector2 moveDirection;

    [SerializeField]
    private float minPitch;

    [SerializeField] 
    private float maxPitch;

    Transform cameraPivot;

    public override void Start()
    {
        base.Start();
        cameraPivot = cameraHandler.GetCameraPivot();

        if (cameraPivot == null)
        {
            Debug.LogWarning("Camera pivot is null");
            return;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleMouseMove();
        }

        HandleCameraMove();
    }

    private void HandleMouseMove()
    {
        moveDirection.y += Input.GetAxis("Mouse X") * rotateSpeed;
        moveDirection.x -= Input.GetAxis("Mouse Y") * rotateSpeed;

        moveDirection.x = Mathf.Clamp(moveDirection.x, minPitch, maxPitch);
    }

    private void HandleCameraMove()
    {
        Quaternion rotation = Quaternion.Euler(moveDirection.x, moveDirection.y, 0);
        cameraPivot.rotation = Quaternion.Lerp(cameraPivot.rotation, rotation, rotateSpeed * Time.deltaTime);

        Vector3 rot = cameraPivot.rotation.eulerAngles;
        rot.z = 0;

        cameraPivot.rotation = Quaternion.Euler(rot);
    }
}
