using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for usage in car configurator.
/// </summary
[System.Serializable/*, CreateAssetMenu(menuName = "Tuning/Car")*/]
public class Car : TuningList
{
    public string carDescription;
    public int year;
}


