using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Tuning/Component")]
public class TuningComponent : TuningAppliaple
{
    public GameObject tuningItemPrefab;

    public override void ApplyTuning(Car carObject, TuningCategory category)
    {
        category.InstantiateTuning(tuningItemPrefab);
    }
}
