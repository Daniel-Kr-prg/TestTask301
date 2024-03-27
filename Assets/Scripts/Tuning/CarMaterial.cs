using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(menuName = "Tuning/Paint")]
public class CarMaterial : TuningAppliaple
{
    public Material material;

    public override void ApplyTuning(Car carObject, TuningCategory category)
    {
        carObject.selectedMaterial = material;
    }
}
