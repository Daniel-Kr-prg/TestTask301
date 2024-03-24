using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TuningBase : ScriptableObject
{
    [HideInInspector]
    public bool isSelected;

    public Description description;
}
