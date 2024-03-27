using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : TuningCategory
{
    /// <summary>
    /// Material which is set on actual Car GameObject. This material wil be affected by Paint tuning
    /// </summary>
    [SerializeField]
    public Material carPaintMaterial;

    /// <summary>
    /// Current selected material used as target material for paint tuning
    /// </summary>
    [HideInInspector]
    public Material selectedMaterial;

    /// <summary>
    /// Current selected category of tuning
    /// </summary>
    private TuningCategory selectedCategory;

    /// <summary>
    /// Apply tuning in the category by index
    /// </summary>
    public void ApplyTuningItem(TuningCategory tuningCategory, int index)
    {
        if (tuningCategory == null)
            return;

        tuningCategory.SelectTuningItem(this, index);
    }

    /// <summary>
    /// Apply specified tuning item in the category
    /// </summary>
    public void ApplyTuningItem(TuningCategory tuningCategory, TuningAppliaple item)
    {
        if (tuningCategory == null)
            return;

        tuningCategory.SelectTuningItem(this, item);
    }

    /// <summary>
    /// Set default tuning item in each category
    /// </summary>
    public void SetDefaults()
    {
        foreach (TuningCategory category in GetComponentsInChildren<TuningCategory>()) 
        {
            category.SetDefault(this);
        }
    }
}

