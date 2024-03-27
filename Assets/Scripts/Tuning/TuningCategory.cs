using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class TuningCategory : TuningList
{

    [Header("Tuning items"), SerializeField]
    private TuningAppliaple defaultItem;

    [Space]
    public List<TuningAppliaple> tuningItems = new List<TuningAppliaple>();

    [SerializeField, Space]
    private Attachment attachment;

    public void ApplyDefaults()
    {
        if (defaultItem != null)
            SetSelectedItem(defaultItem);

        foreach (TuningCategory category in categories)
        {
            category.ApplyDefaults();
        }
    }
    
    public void SetSelectedItem(TuningAppliaple newSelected)
    {
        foreach (TuningAppliaple item in tuningItems)
        {
            item.isSelected = false;
        }
        newSelected.isSelected = true;
    }

    public TuningAppliaple GetDefaultItem()
    {
        return defaultItem;
    }

    public Attachment GetAttachmentPoint()
    {
        return attachment;
    }
    //public TuningComponent GetComponentByCode(string code)
    //{
    //    return components.Find(x => x.description.code.Equals(code));
    //}
}

[System.Serializable]
public class TuningList
{
    public string name;
    public LocalizedString localStr;

    public List<TuningCategory> categories = new List<TuningCategory>();
    
    [Header("Description")]
    public Sprite preview;
}