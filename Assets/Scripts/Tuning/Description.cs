using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Description
{
    public string name;
    public Sprite preview;
}

[System.Serializable]
public class CarDescription : Description
{
    public string description;
    public string year;
}
