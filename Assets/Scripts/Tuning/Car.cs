using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for usage in car configurator.
/// </summary
[System.Serializable/*, CreateAssetMenu(menuName = "Tuning/Car")*/]
public class Car : TuningList
{
    public CarMaterial defaultMaterial;

    //public TuningCategory GetCategoryByCode(string code)
    //{
    //    string[] codes = code.Split('.');

    //    if (codes.Length == 1)
    //    {
    //        return tuningCategories.Find(x => x.description.code.Equals(code));
    //    }

    //    TuningCategory currentCategory = null;
    //    foreach (string codeString in codes)
    //    {
    //        currentCategory = tuningCategories.Find(x => x.description.code.Equals(code));
    //        if (currentCategory != null)
    //        {
    //            currentCategory = currentCategory.innerCategories.Find(x => x.description.code.Equals(code));
    //        }
    //        else
    //        {
    //            Debug.LogError("Category code is not correct");
    //            return null;
    //        }
    //    }
    //    return currentCategory;
    //}
}


