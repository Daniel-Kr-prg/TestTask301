using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TuningBase : ScriptableObject
{
    public bool isSelected;

    public Description description;
    public virtual void GetElementType(ITuningElementsHandler tuningHandler) { }
}
