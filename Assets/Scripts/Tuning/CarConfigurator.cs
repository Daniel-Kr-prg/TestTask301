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
    
    private CarObject selectedCar;

    private TuningCategory selectedCategory;

    private List<GameObject> _carList = new List<GameObject>();
    
    private Car car
    {
        get { return selectedCar?.GetCar(); }
    }


    private void Start()
    {
        List<GameObject> cars = Resources.LoadAll<GameObject>("Cars").ToList();

        foreach (GameObject car in cars)
        {
            if (car.GetComponent<CarObject>() == null)
                continue;

            _carList.Add(car);
        }

        SetCar(0);
        ConfiguratorUI.CreateCarSelectionUI();
    }
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

        if (selectedCar != null)
        {
            Destroy(selectedCar.gameObject);
        }

        selectedCar = Instantiate(newCar, carContainer).GetComponent<CarObject>();

        selectedCar.SetDefaults();
        ResetMaterial();
    }

    public CarObject GetSelectedCar()
    {
        return selectedCar;
    }

    public List<GameObject> GetCarList()
    {
        GameObject[] returnList = new GameObject[_carList.Count];
        _carList.CopyTo(returnList);

        return returnList.ToList();
    }
   

    public void SelectCategory(TuningCategory category)
    {
        selectedCategory = category;
    }

    public void ApplyTuning(TuningAppliaple item)
    {
        selectedCar.ApplyTuningItem(selectedCategory, item);
    }

    private void ResetMaterial()
    {
        if (selectedCar.selectedMaterial == null)
            return;

        selectedCar.carPaintMaterial.CopyPropertiesFromMaterial(selectedCar.selectedMaterial);
    }

    public void Update()
    {
        if (selectedCar == null || selectedCar.selectedMaterial == null)
        {
            return;
        }

        selectedCar.carPaintMaterial.Lerp(selectedCar.carPaintMaterial, selectedCar.selectedMaterial, materialChangeSpeed);
    }
}
