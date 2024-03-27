using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TuningAppliaple : TuningBase
{
    public virtual void ApplyTuning(Car carObject, TuningCategory tuningCategory) { }
    public virtual void RemoveTuning(Car carObject, TuningCategory tuningCategory) { }

    public override void Select()
    {
        isSelected = true;
    }

    public override void Deselect()
    {
        isSelected = false;
    }
}