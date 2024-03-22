using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraZoomHandler : CameraFeature
{
    [SerializeField]
    private float zoomSpeed = 5f;

    [SerializeField]
    private float zoomMultiplier = 5f;

    private float targetZoomValue;


    private Transform cameraTransform;

    public override void Start()
    {
        base.Start();
        cameraTransform = cameraHandler.GetOrbitingCamera().transform;
        targetZoomValue = cameraTransform.position.z;
    }

    protected override void OnMouseScrollHandle()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f) // forward
        {
            targetZoomValue += scroll * zoomMultiplier;
        }
    }

    protected override void ApplyEffectToCamera()
    {
        if (options.IsZoomClampAllowed)
        {
            targetZoomValue = Mathf.Clamp(targetZoomValue, options.MinZoomDistance, options.MaxZoomDistance);
        }

        float distance = Mathf.Lerp(cameraTransform.localPosition.z, targetZoomValue, zoomSpeed * Time.deltaTime);

        cameraTransform.localPosition = new Vector3(
            cameraTransform.localPosition.x,
            cameraTransform.localPosition.y,
            distance
        );
    }
}
