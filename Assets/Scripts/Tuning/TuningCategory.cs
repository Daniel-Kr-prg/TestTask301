using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class TuningCategory : TuningList
{
    [SerializeField]
    private TuningAppliaple defaultItem;

    public List<TuningAppliaple> tuningItems = new List<TuningAppliaple>();

    [SerializeField]
    private Attachment attachment;

    public void ApplyDefaults()
    {
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
    public Description description;
    public List<TuningCategory> categories = new List<TuningCategory>();
}