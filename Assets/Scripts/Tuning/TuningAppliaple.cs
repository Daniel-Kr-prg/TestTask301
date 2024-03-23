using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TuningAppliaple : TuningBase
{
    public bool isDefault;
    public virtual void ApplyTuning(CarConfigurator configurator) { }
}