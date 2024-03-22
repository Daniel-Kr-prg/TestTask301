using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarScript : MonoBehaviour
{
    [SerializeField]
    public Material carPaintMaterial;

    [SerializeField]
    private Car car;

    public Car GetCar()
    {
        return car;
    }
}
