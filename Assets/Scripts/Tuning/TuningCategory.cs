using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuningCategory : MonoBehaviour
{
    /// <summary>
    /// Inner tuning categories
    /// </summary>
    [SerializeField]
    private List<TuningCategory> childCategories;

    /// <summary>
    /// Containers where tuning prefabs are instantiated
    /// </summary>
    [SerializeField]
    private List<GameObject> tuningInstanceContainers;

    /// <summary>
    /// Tuning category data scriptable object
    /// </summary>
    [SerializeField]
    private TuningCategoryData tuningCategoryData;

    /// <summary>
    /// Focus point used by camera handler
    /// </summary>
    [SerializeField]
    CameraTargetOptions cameraTarget;

    public CameraTargetOptions GetCameraTarget()
    {
        return cameraTarget;
    }

    public void InstantiateTuning(GameObject tuningPrefab)
    {
        foreach (GameObject t in tuningInstanceContainers)
        {
            foreach (Transform child in t.transform)
            {
                Destroy(child.gameObject);
            }

            if (tuningPrefab != null)
                Instantiate(tuningPrefab, t.transform);
        }
    }

    public TuningCategoryData GetCategoryData()
    {
        return tuningCategoryData;
    }

    public List<TuningCategory> GetChildCategories()
    {
        return childCategories;
    }

    public void SelectTuningItem(Car car, int itemIndex)
    {
        if (tuningCategoryData == null)
        {
            return;
        }

        TuningAppliaple item = tuningCategoryData.tuningItems[Mathf.Clamp(itemIndex, 0, tuningCategoryData.tuningItems.Count - 1)];
        SelectTuningItem(car, item);
    }

    public void SelectTuningItem(Car car, TuningAppliaple item)
    {
        if (tuningCategoryData == null)
        {
            return;
        }

        tuningCategoryData.GetSelectedItem()?.RemoveTuning(car, this);
        tuningCategoryData.SetSelectedItem(item);
        item?.ApplyTuning(car, this);
    }

    public void SetDefault(Car car)
    {
        if (tuningCategoryData == null)
        {
            return;
        }

        SelectTuningItem(car, tuningCategoryData.GetDefaultItem());
    }
}
