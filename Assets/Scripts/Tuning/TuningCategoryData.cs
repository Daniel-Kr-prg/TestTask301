using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable, CreateAssetMenu(menuName = "Tuning/Category")]
public class TuningCategoryData : TuningBase
{
    [Header("Tuning items"), SerializeField]
    private TuningAppliaple defaultItem;

    private TuningAppliaple selectedItem;

    [Space]
    public List<TuningAppliaple> tuningItems = new List<TuningAppliaple>();

    public void ApplyDefaultItem()
    {
        if (defaultItem != null)
            SetSelectedItem(defaultItem);
    }
    
    public void SetSelectedItem(TuningAppliaple newSelected)
    {
        selectedItem?.Deselect();

        selectedItem = newSelected;
        newSelected?.Select();
    }

    public TuningAppliaple GetSelectedItem()
    {
        return selectedItem;
    }

    public TuningAppliaple GetDefaultItem()
    {
        return defaultItem;
    }
}
