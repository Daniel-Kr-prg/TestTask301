using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFeature : MonoBehaviour
{
    protected CameraHandler cameraHandler;

    public virtual void Start()
    {
        cameraHandler = GetComponent<CameraHandler>();
        if (cameraHandler == null)
        {
            Debug.LogError("Camera Feature must be set on Camera Handler object");
            Destroy(this);
            return;
        }
    }
}
