using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> attachmentInstanceContainers;

    [SerializeField]
    CameraTargetOptions cameraTarget;

    public CameraTargetOptions GetCameraTarget()
    {
        return cameraTarget;
    }

    public virtual void SetAttachment(GameObject attachmentPrefab)
    {
        foreach (GameObject t in attachmentInstanceContainers)
        {
            foreach (Transform child in t.transform)
            {
                Destroy(child.gameObject);
            }

            if (attachmentPrefab != null)
                Instantiate(attachmentPrefab, t.transform);
        }
    }
}
