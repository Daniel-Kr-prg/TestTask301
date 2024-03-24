using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Tuning/Component")]
public class TuningComponent : TuningAppliaple, ITuningElement
{
    public GameObject tuningItemPrefab;

    public override void ApplyTuning(CarObject carObject)
    {
        carObject.Apply(this);
    }

    public void GetElementType(ITuningElementsHandler tuningHandler)
    {
        tuningHandler.HandleElementType(this);
    }
}
