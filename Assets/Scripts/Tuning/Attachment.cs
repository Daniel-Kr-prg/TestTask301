using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField]
    private List<Transform> attachmentInstanceContainers;

    [SerializeField]
    CameraTargetOptions cameraTarget;

    public CameraTargetOptions GetCameraTarget()
    {
        return cameraTarget;
    }

    public virtual void SetAttachment(GameObject attachmentPrefab)
    {
        foreach (Transform t in attachmentInstanceContainers)
        {
            if (t.childCount != 0)
            {
                Destroy(t.GetChild(0).gameObject);
            }

            Instantiate(attachmentPrefab, t);
        }
    }
}
