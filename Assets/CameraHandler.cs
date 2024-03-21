using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CameraHandler : MonoBehaviour
{
    private bool _cameraHandlerIsAvailable = false;
    public static CameraHandler instance;

    [Header("Camera settings")]
    [SerializeField]
    private Camera orbitingCamera;

    [Header("Target"), SerializeField]
    private Transform defaultTarget;
    
    [SerializeField]
    private Transform target;

    [Header("AFK Handle")]
    [SerializeField]
    private float timeToGoIdle;

    private float currentIdleTime;

    public CameraState State
    {
        get
        {
            return _cameraState;
        }
        set
        {
            if (_cameraState != value)
            {
                cameraStateChanged.Invoke(_cameraState);
            }
            _cameraState = value;
        }
    }
    private CameraState _cameraState;
    
    public UnityEvent<CameraState> cameraStateChanged;

    void Start()
    {
        if (defaultTarget == null)
        {
            Debug.LogError("Target object was not set.");
            return;
        }
        if (orbitingCamera == null)
        {
            Debug.LogError("Target Camera was not set.");
            return;
        }
        if (target == null)
        {
            target = defaultTarget;
            Debug.LogWarning("Target is null. Default target was chosen");
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There must be only one camera handler on scene. Destroying new instance");
            Destroy(gameObject);
        }
        UpdateIdleTime();

        _cameraHandlerIsAvailable = true;
    }

    private void Update()
    {
        if (!_cameraHandlerIsAvailable )
        {
            return;
        }

        if (Input.anyKey)
        {
            UpdateIdleTime();
        }

        currentIdleTime -= Time.deltaTime;
        UpdateCameraState();
    }

    public void ChangeTarget(Transform newTarget)
    {
        if (newTarget == null)
        {
            Debug.LogWarning("New target value is null, can't set new camera target.");
            return;
        }

        target = newTarget;
    }

    private void UpdateIdleTime()
    {
        currentIdleTime = timeToGoIdle;
    }

    private void UpdateCameraState()
    {
        if (currentIdleTime <= 0)
        {
            State = CameraState.Idle;
        }
        else
        {
            State = CameraState.Active;
        }
    }
}

public enum CameraState
{
    Active, Idle
}
