using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationHandler : CameraFeature
{
    [SerializeField]
    private float _rotateSpeed = 5f;

    [SerializeField]
    private float _mouseMoveMultiplier = 5f;

    [SerializeField]
    private float _idlePitch;

    [SerializeField]
    private float _idleYawSpeed;

    private bool _mouseIsDown = false;


    private Vector2 _targetRotation;

    protected override void ApplyEffectToCamera()
    {
        if (cameraHandler.state == CameraState.Active)
        {
            if (options.IsPitchClampAllowed)
            {
                _targetRotation.x = Mathf.Clamp(_targetRotation.x, options.MinPitch, options.MaxPitch);
            }

            if (options.IsYawClampAllowed)
            {
                _targetRotation.y = Mathf.Clamp(_targetRotation.y, options.MinYaw, options.MaxYaw);
            }
        }
        else
        {
            _targetRotation.y += _idleYawSpeed * Time.deltaTime;
        }

        Quaternion rotation = Quaternion.Euler(_targetRotation.x, _targetRotation.y, 0);
        cameraPivot.rotation = Quaternion.Lerp(cameraPivot.rotation, rotation, _rotateSpeed * Time.deltaTime);

        Vector3 rot = cameraPivot.rotation.eulerAngles;
        rot.z = 0;

        cameraPivot.rotation = Quaternion.Euler(rot);
    }

    protected override void OnMouseMoveHandle()
    {
        if (!_mouseIsDown)
        {
            return;
        }

        _targetRotation.y += Input.GetAxis("Mouse X") * _mouseMoveMultiplier;
        _targetRotation.x -= Input.GetAxis("Mouse Y") * _mouseMoveMultiplier;
    }

    protected override void OnMouseDownHandle()
    {
        _mouseIsDown = true;
    }

    protected override void OnMouseUpHandle()
    {
        _mouseIsDown = false;
    }

    protected override void OnTargetChanged()
    {
        _targetRotation.x = options.DefaultPitch;
        _targetRotation.y = options.DefaultYaw;
    }

    protected override void OnCameraStateChanged(CameraState state)
    {
        _targetRotation.x = _idlePitch;
    }
}
