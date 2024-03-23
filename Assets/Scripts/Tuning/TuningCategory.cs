using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(menuName = "Tuning/Category")]
public class TuningCategory : TuningBase
{
    public List<TuningBase> elements = new List<TuningBase>();

    public Attachment categoryAttachment;

    public override void GetElementType(ITuningElementsHandler tuningHandler)
    {
        tuningHandler.HandleElementType(this);
    }

    //public TuningComponent GetComponentByCode(string code)
    //{
    //    return components.Find(x => x.description.code.Equals(code));
    //}
}
