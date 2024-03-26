using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarObject : MonoBehaviour
{
    [SerializeField]
    public Material carPaintMaterial;

    [HideInInspector]
    public Material selectedMaterial;

    [SerializeField]
    private Car car;

    private TuningCategory selectedCategory;

    public void ApplyTuningItem(TuningCategory tuningCategory, TuningAppliaple tuningItem)
    {
        if (tuningCategory == null)
            return;

        if (tuningItem == null)
            return;

        selectedCategory = tuningCategory;
        tuningItem.ApplyTuning(this);
    }

    public void Apply(TuningComponent tuningItem)
    {
        selectedCategory.SetSelectedItem(tuningItem);
        selectedCategory.GetAttachmentPoint().SetAttachment(tuningItem.tuningItemPrefab);
    }

    public void Apply(CarMaterial material)
    {
        selectedCategory.SetSelectedItem(material);
        selectedMaterial = material.material;
    }

    public void SetDefaults()
    {
        foreach (TuningCategory category in car.categories)
        {
            category.ApplyDefaults();
            ApplyTuningItem(category, category.GetDefaultItem());
        }
    }

    public Car GetCar()
    {
        return car;
    }
}

