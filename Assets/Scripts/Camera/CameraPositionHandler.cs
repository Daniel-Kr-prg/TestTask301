using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionHandler : CameraFeature
{
    [SerializeField]
    private float moveSpeed = 5f;

    protected override void ApplyEffectToCamera()
    {
        cameraPivot.position = Vector3.Lerp(cameraPivot.position, cameraHandler.GetTargetPosition(), moveSpeed * Time.deltaTime);
    }
}