using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraZoomHandler : CameraFeature
{
    [SerializeField]
    private float defaultDistance;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float maxDistance;

    [Range(0f, 1f)]
    private float distance;
}
