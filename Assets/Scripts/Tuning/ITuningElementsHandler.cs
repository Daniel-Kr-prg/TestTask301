using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITuningElementsHandler
{
    public void HandleElementType(TuningComponent tuningComponent);
    public void HandleElementType(CarMaterial carMaterial);
    public void HandleElementType(TuningCategory tuningCategory);
}