using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tuning/Component")]
public class TuningComponent : TuningAppliaple
{
    public GameObject tuningItemPrefab;

    public override void ApplyTuning(CarConfigurator configurator)
    {
        configurator.Apply(this);
    }

    public override void GetElementType(ITuningElementsHandler tuningHandler)
    {
        tuningHandler.HandleElementType(this);
    }
}
