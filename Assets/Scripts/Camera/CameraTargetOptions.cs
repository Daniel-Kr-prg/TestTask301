using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetOptions : MonoBehaviour
{
    [Header("Pitch settings"), SerializeField, Space]
    private bool _pitchClampAllowed = false;

    [SerializeField]
    private float _minPitch;

    [SerializeField]
    private float _maxPitch;

    [SerializeField]
    private float _defaultPitch;

    [Header("Yaw settings"), SerializeField, Space]
    private bool _yawClampAllowed = false;

    [SerializeField]
    private float _minYaw;

    [SerializeField]
    private float _maxYaw;

    [SerializeField]
    private float _defaultYaw;

    [Header("Zoom clamp settings"), SerializeField, Space]
    private bool _zoomClampAllowed = false;

    [SerializeField, Space]
    private float _minDistance;

    [SerializeField]
    private float _maxDistance;

    [SerializeField]
    private float _defaultDistance;

    public bool IsPitchClampAllowed
    {
        get { return _pitchClampAllowed; }
    }

    public bool IsYawClampAllowed
    {
        get { return _yawClampAllowed; }
    }

    public bool IsZoomClampAllowed
    {
        get { return _zoomClampAllowed; }
    }

    public float MinPitch
    {
        get { return _minPitch; }
    }

    public float MaxPitch
    {
        get { return _maxPitch; }
    }

    public float DefaultPitch
    {
        get { return _defaultPitch; }
    }

    public float MinYaw
    {
        get { return _minYaw; }
    }

    public float MaxYaw
    {
        get { return _maxYaw; }
    }

    public float DefaultYaw
    {
        get { return _defaultYaw; }
    }

    public float MinZoomDistance
    {
        get { return _minDistance; }
    }
    public float MaxZoomDistance
    {
        get { return _maxDistance; }
    }

    public float DefaultZoomDistance
    {
        get { return _defaultDistance; }
    }
}
