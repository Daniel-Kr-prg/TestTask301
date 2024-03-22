using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for usage in car configurator.
/// </summary>
[System.Serializable]
public class Car
{
    [System.Serializable]
    public class CarData
    {
        public string carCode;

        public string name;
        public string description;
        public string year;
    }

    public CarData carData;

    public Tuning tuning;
}



[System.Serializable]
public class Tuning
{
    [HideInInspector]
    public CarMaterial selectedMaterial;

    public List<CarMaterial> carMaterials;

    public List<TuningCategory> tuningCategories;
}

[System.Serializable]
public class TuningCategory
{
    [System.Serializable]
    public class TuningCategoryData
    {
        public string categoryCode;
        public string name;
    }

    public TuningCategoryData categoryData;

    [HideInInspector]
    public TuningComponent selectedComponent;

    public List<TuningComponent> components;

    public Attachment categoryAttachment;
}

[System.Serializable]
public class TuningComponent
{
    [System.Serializable]
    public class TuningComponentData
    {
        public string componentCode;
        public string name;
    }

    public TuningComponentData componentData;

    public GameObject componentPrefab;
}

[System.Serializable]
public class CarMaterial
{
    public string materialCode;
    public string materialName;

    public Material material;
}

//[System.Serializable]
//public class CarConfiguration 
//{
//    public Car.CarData carData;
//    public List<TuningCategoryConfiguration> categories;
//}

//[System.Serializable]
//public class TuningCategoryConfiguration
//{
//    public TuningCategory.TuningCategoryData categoryData;

//    public TuningComponent.TuningComponentData selectedComponent;
//}
