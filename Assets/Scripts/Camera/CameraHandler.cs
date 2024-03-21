using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CameraHandler : MonoBehaviour
{
    /// <summary>
    /// Controls the availability of camera handler. <br>If false, then can't handle camera state</br>
    /// </summary>
    private bool _cameraHandlerIsAvailable = false;

    /// <summary>
    /// Singleton object for camera handler
    /// </summary>
    public static CameraHandler instance;

    /// <summary>
    /// Camera to orbit around
    /// </summary>
    [Header("Camera settings")]
    [SerializeField]
    private Camera _orbitingCamera;

    /// <summary>
    /// The point camera rotates around
    /// </summary>
    [SerializeField]
    private Transform _cameraPivot;

    /// <summary>
    /// Default camera target
    /// </summary>
    [Header("Target"), SerializeField]
    private Transform _defaultTarget;

    /// <summary>
    /// Current camera target
    /// </summary>
    [SerializeField]
    private Transform _target;

    /// <summary>
    /// Time for camera to go idle
    /// </summary>
    [Header("AFK Handle")]
    [SerializeField]
    private float _timeToGoIdle;

    /// <summary>
    /// Current camera idle time. If less than 0, then camera goes idle
    /// </summary>
    private float _currentIdleTime;

    public CameraState state
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There must be only one camera handler on scene. Destroying new instance");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (_defaultTarget == null)
        {
            Debug.LogError("Default Target object was not set.");
            return;
        }
        if (_orbitingCamera == null)
        {
            Debug.LogError("Target Camera was not set.");
            return;
        }
        _cameraHandlerIsAvailable = true;

        UpdateIdleTime();
        
        if (_target == null)
        {
            SetTargetToDefault();
            Debug.LogWarning("Target is null. Default target was chosen");
        }
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

        _currentIdleTime -= Time.deltaTime;
        UpdateCameraState();
    }

    public Transform GetCameraPivot()
    {
        return _cameraPivot;
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
        {
            Debug.LogWarning("New target value is null, can't set new camera target.");
            return;
        }

        _target = newTarget;
    }

    public Transform GetTarget()
    {
        return _target.transform;
    }

    public Vector3 GetTargetPosition()
    {
        return new Vector3(_target.position.x, _target.position.y, _target.position.z);
    }

    public void SetTargetToDefault()
    {
        if (!_cameraHandlerIsAvailable)
        {
            Debug.LogWarning("Camera Handler was not initialized");
            return;
        }
        SetTarget(_defaultTarget);
    }

    public void SetDefaultTarget(Transform newDefaultTarget)
    {
        if (newDefaultTarget == null)
        {
            Debug.LogWarning("New target value is null, can't set new camera target.");
            return;
        }

        _defaultTarget = newDefaultTarget;
    }

    private void UpdateIdleTime()
    {
        _currentIdleTime = _timeToGoIdle;
    }

    private void UpdateCameraState()
    {
        if (_currentIdleTime <= 0)
        {
            state = CameraState.Idle;
        }
        else
        {
            state = CameraState.Active;
        }
    }

    public Camera GetOrbitingCamera()
    {
        return _orbitingCamera;
    }
}

public enum CameraState
{
    Active, Idle
}
