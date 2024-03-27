using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

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
    private CameraTargetOptions _defaultTarget;

    /// <summary>
    /// Current camera target
    /// </summary>
    [SerializeField]
    private Transform _targetTransform;

    /// <summary>
    /// target options for camera
    /// </summary>
    public CameraTargetOptions cameraTargetOptions { get; private set; }

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

    /// <summary>
    /// Event used by camera handler features to invoke mouse down
    /// </summary>
    [HideInInspector]
    public UnityEvent mouseDownEvent = new UnityEvent();

    /// <summary>
    /// Event used by camera handler features to invoke mouse up
    /// </summary>
    [HideInInspector]
    public UnityEvent mouseUpEvent = new UnityEvent();

    /// <summary>
    /// Event used by camera handler features to invoke mouse scroll
    /// </summary>
    [HideInInspector]
    public UnityEvent mouseScrollEvent = new UnityEvent();

    /// <summary>
    /// Event used by camera handler features to invoke mouse move
    /// </summary>
    [HideInInspector]
    public UnityEvent mouseMoveEvent = new UnityEvent();

    /// <summary>
    /// returns camera state. On state change invokes CameraStateChanged event
    /// </summary>
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

    /// <summary>
    /// Event invoked on state change
    /// </summary>
    [HideInInspector]
    public UnityEvent<CameraState> cameraStateChanged;

    /// <summary>
    /// Event invoked by changing target
    /// </summary>
    [HideInInspector]
    public UnityEvent targetChanged;

    private void Awake()
    {
        // Set CameraHandler as singleton object

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There must be only one camera handler on scene. Destroying new instance");
            Destroy(gameObject);
        }

        // Handle null objects
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

        // Restart idle timer
        UpdateIdleTime();

        // if no specific target was set, then use default target
        if (_targetTransform == null)
        {
            SetTargetToDefault();
        }
    }

    private void Update()
    {
        if (!_cameraHandlerIsAvailable )
        {
            return;
        }

        // Check if any control was pressed and update idle time
        if (Input.anyKey)
        {
            UpdateIdleTime();
        }

        _currentIdleTime -= Time.deltaTime;
        UpdateCameraState();
    }

    /// <summary>
    /// </summary>
    /// <returns>Camera pivot point, which is it orbiting around</returns>
    public Transform GetCameraPivot()
    {
        return _cameraPivot;
    }

    /// <summary>
    /// </summary>
    /// <returns>Handled camera object</returns>
    public Camera GetOrbitingCamera()
    {
        return _orbitingCamera;
    }

    /// <summary>
    /// Set new target for camera to orbit around
    /// </summary>
    /// <param name="newTarget">GameObject with CameraTargetOptions component</param>
    public void SetTarget(CameraTargetOptions newTarget)
    {
        if (newTarget == null)
        {
            SetTargetToDefault();
            return;
        }


        cameraTargetOptions = newTarget;
        _targetTransform = newTarget.transform;

        targetChanged?.Invoke();
    }

    /// <summary>
    /// </summary>
    /// <returns>Target transform</returns>
    public Transform GetTarget()
    {
        if (_targetTransform == null)
            SetTargetToDefault();

        return _targetTransform.transform;
    }

    /// <summary>
    /// </summary>
    /// <returns>Target transform position copy</returns>
    public Vector3 GetTargetPosition()
    {
        if (_targetTransform == null)
            SetTargetToDefault();

        return new Vector3(_targetTransform.position.x, _targetTransform.position.y, _targetTransform.position.z);
    }

    /// <summary>
    /// Set current target to the default target
    /// </summary>
    public void SetTargetToDefault()
    {
        if (!_cameraHandlerIsAvailable || _defaultTarget == null)
        {
            Debug.LogWarning("Camera Handler was not initialized");
            return;
        }
        SetTarget(_defaultTarget);
    }

    /// <summary>
    /// Change default target
    /// </summary>
    /// <param name="newDefaultTarget">GameObject with CameraTargetOptions component</param>
    public void SetDefaultTarget(CameraTargetOptions newDefaultTarget)
    {
        if (newDefaultTarget == null)
        {
            Debug.LogWarning("New target value is null, can't set new camera target.");
            return;
        }

        _defaultTarget = newDefaultTarget;
    }

    public Vector3 GetDefaultTargetPosition()
    {
        return new Vector3(_defaultTarget.transform.position.x, _defaultTarget.transform.position.y, _defaultTarget.transform.position.z);
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

    // Events invoke

    public void MouseDownInvoke()
    {
        mouseDownEvent.Invoke();
    }

    public void MouseUpInvoke()
    {
        mouseUpEvent.Invoke();
    }

    public void MouseMoveInvoke()
    {
        mouseMoveEvent.Invoke();
    }

    public void MouseScrollInvoke()
    {
        mouseScrollEvent.Invoke();
    }
}


public enum CameraState
{
    Active, Idle
}
