using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TuningBase : ScriptableObject
{
    public string itemName;
    public Sprite preview;

    [HideInInspector]
    public bool isSelected;
}
