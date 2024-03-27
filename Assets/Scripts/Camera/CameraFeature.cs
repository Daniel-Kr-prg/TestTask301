using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFeature : MonoBehaviour
{
    protected CameraHandler cameraHandler;
    protected Transform cameraPivot;

    protected CameraTargetOptions options
    {
        get
        {
            return cameraHandler.cameraTargetOptions;
        }
    }

    private bool effectIsActive = true;

    private bool _initialized;

    public virtual void Start()
    {
        _initialized = false;

        cameraHandler = GetComponent<CameraHandler>();
        if (cameraHandler == null)
        {
            Debug.LogError("Camera Feature must be set on Camera Handler object");
            return;
        }

        cameraPivot = cameraHandler.GetCameraPivot();
        if (cameraPivot == null)
        {
            Debug.LogWarning("Camera pivot is null");
            return;
        }

        cameraHandler.mouseDownEvent?.AddListener(OnMouseDownHandle);
        cameraHandler.mouseUpEvent?.AddListener(OnMouseUpHandle);
        cameraHandler.mouseMoveEvent?.AddListener(OnMouseMoveHandle);
        cameraHandler.mouseScrollEvent?.AddListener(OnMouseScrollHandle);
        cameraHandler.targetChanged?.AddListener(OnTargetChanged);
        cameraHandler.cameraStateChanged?.AddListener(OnCameraStateChanged);

        OnTargetChanged();

        _initialized = true;
    }

    private void Update()
    {
        if (_initialized) 
        { 
            ApplyEffectToCamera();
        }
    }

    protected virtual void ApplyEffectToCamera() { }
    protected virtual void OnMouseScrollHandle() { }
    protected virtual void OnMouseDownHandle() { }
    protected virtual void OnMouseUpHandle() { }
    protected virtual void OnMouseMoveHandle() { }
    protected virtual void OnTargetChanged() { }
    protected virtual void OnCameraStateChanged(CameraState state) { }
}
