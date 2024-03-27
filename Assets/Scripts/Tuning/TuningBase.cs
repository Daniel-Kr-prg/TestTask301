using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public abstract class TuningBase : ScriptableObject
{
    public string itemName;
    public LocalizedString localStr;
    public Sprite preview;
    public bool isSelected = false;

    public virtual void Select() { }
    public virtual void Deselect() { }
}
