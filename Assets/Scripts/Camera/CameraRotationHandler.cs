using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationHandler : CameraFeature
{
    [SerializeField]
    private float _rotateSpeed = 5f;

    [SerializeField]
    private float _mouseMoveMultiplier = 5f;

    private bool _mouseIsDown = false;


    private Vector2 _moveDirection;

    protected override void ApplyEffectToCamera()
    {
        if (options.IsPitchClampAllowed)
        {
            _moveDirection.x = Mathf.Clamp(_moveDirection.x, options.MinPitch, options.MaxPitch);
        }

        if (options.IsYawClampAllowed)
        {
            _moveDirection.y = Mathf.Clamp(_moveDirection.y, options.MinYaw, options.MaxYaw);
        }

        Quaternion rotation = Quaternion.Euler(_moveDirection.x, _moveDirection.y, 0);
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

        _moveDirection.y += Input.GetAxis("Mouse X") * _mouseMoveMultiplier;
        _moveDirection.x -= Input.GetAxis("Mouse Y") * _mouseMoveMultiplier;
    }

    protected override void OnMouseDownHandle()
    {
        _mouseIsDown = true;
    }

    protected override void OnMouseUpHandle()
    {
        _mouseIsDown = false;
    }
}
