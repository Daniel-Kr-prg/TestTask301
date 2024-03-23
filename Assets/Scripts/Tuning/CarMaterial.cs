using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tuning/Paint")]
public class CarMaterial : TuningAppliaple
{
    public Material material;

    public override void ApplyTuning(CarConfigurator configurator)
    {
        configurator.Apply(this);
    }

    public override void GetElementType(ITuningElementsHandler tuningHandler)
    {
        tuningHandler.HandleElementType(this);
    }
}
