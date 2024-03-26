using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(menuName = "Tuning/Paint")]
public class CarMaterial : TuningAppliaple
{
    public Material material;

    public override void ApplyTuning(CarObject carObject)
    {
        carObject.Apply(this);
    }
}
