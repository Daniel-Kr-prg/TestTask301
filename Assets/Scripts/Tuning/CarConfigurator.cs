using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CarConfigurator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField, Space]
    private Transform carContainer;

    [SerializeField]
    private UIHandler ConfiguratorUI;


    [Header("Car paint settings")]

    [SerializeField]
    private float materialChangeSpeed;
    
    private Car currentCar;

    private TuningCategory selectedCategory;

    private List<GameObject> _carList = new List<GameObject>();

    private void Start()
    {
        List<GameObject> cars = Resources.LoadAll<GameObject>("Cars").ToList();

        foreach (GameObject car in cars)
        {
            if (car.GetComponent<Car>() == null)
                continue;

            _carList.Add(car);
        }

        SetCar(0);
        ConfiguratorUI.CreateCarSelectionUI();
    }

    /// <summary>
    /// Set the car for configurator by its index
    /// </summary>
    public void SetCar(int carIndex)
    {
        if (_carList == null)
        {
            Debug.LogError("Can't set car, car list is not set");
            return;
        }
        if (carIndex < 0 || carIndex >= _carList.Count)
        {
            Debug.LogError("Can't set car, car index is out of bounds");
            return;
        }
        GameObject newCar = _carList[carIndex];

        if (currentCar != null)
        {
            Destroy(currentCar.gameObject);
        }

        currentCar = Instantiate(newCar, carContainer).GetComponent<Car>();

        currentCar.SetDefaults();
        ResetMaterial();
    }

    public Car GetSelectedCar()
    {
        return currentCar;
    }

    /// <summary>
    /// Get list of car prefabs
    /// </summary>
    public List<GameObject> GetCarList()
    {
        GameObject[] returnList = new GameObject[_carList.Count];
        _carList.CopyTo(returnList);

        return returnList.ToList();
    }

    /// <summary>
    /// Select the category for tuning
    /// </summary>
    public void SelectCategory(TuningCategory category)
    {
        selectedCategory = category;
    }
    
    /// <summary>
    /// Apply item by its index in currently selected category
    /// </summary>
    public void ApplyTuning(int index)
    {
        currentCar.ApplyTuningItem(selectedCategory, index);
    }

    /// <summary>
    /// Reset car material properties
    /// </summary>
    private void ResetMaterial()
    {
        if (currentCar.selectedMaterial == null)
            return;

        currentCar.carPaintMaterial.CopyPropertiesFromMaterial(currentCar.selectedMaterial);
    }

    public void Update()
    {
        if (currentCar == null || currentCar.selectedMaterial == null)
        {
            return;
        }

        currentCar.carPaintMaterial.Lerp(currentCar.carPaintMaterial, currentCar.selectedMaterial, materialChangeSpeed);
    }
}
