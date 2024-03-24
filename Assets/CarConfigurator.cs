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

    //private CarConfiguration carConfiguration;

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
        //carConfiguration.carCode = car.description.code;
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

    //private void HandleComponentApply(TuningComponent tuningComponent)
    //{
    //    if (tuningComponent == null)
    //    {
    //        Debug.LogError("Can't apply tuning, because tuning item is null");
    //        return;
    //    }

    //    if (tuningComponent.isDefault && carConfiguration.appliedComponents.ContainsKey(selectedCategoryCode))
    //    {
    //        carConfiguration.appliedComponents.Remove(selectedCategoryCode);
    //    }
    //    else
    //    {
    //        if (carConfiguration.appliedComponents.ContainsKey(selectedCategoryCode))
    //        {
    //            carConfiguration.appliedComponents[selectedCategoryCode] = tuningComponent.description.code;
    //        }
    //        else
    //        {
    //            carConfiguration.appliedComponents.Add(selectedCategoryCode, tuningComponent.description.code);
    //        }
    //    }
    //}

    

    public void SelectCategory(TuningCategory category)
    {
        selectedCategory = category;
        CameraHandler.instance.SetTarget(selectedCategory.GetAttachmentPoint()?.GetCameraTarget());
    }

    public void ApplyTuning(TuningAppliaple item)
    {
        selectedCar.ApplyTuningItem(selectedCategory, item);
    }

    private void ResetMaterial()
    {
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
