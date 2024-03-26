using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDescription
{
    public ItemDescription(string n, Sprite p)
    {
        name = n;
        preview = p;
    }

    public string name;
    public Sprite preview;
}
