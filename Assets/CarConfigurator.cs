using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarConfigurator : MonoBehaviour
{
    [SerializeField]
    private CarScript selectedCar;

    [SerializeField, Space]
    private Transform carContainer;

    private List<GameObject> carList = new List<GameObject>();

    [Header("Car paint settings")]

    [SerializeField]
    private float materialChangeSpeed;

    private Car car
    {
        get { return selectedCar?.GetCar(); }
    }


    private void Start()
    {
        List<GameObject> cars = Resources.LoadAll<GameObject>("Cars").ToList();

        foreach (GameObject car in cars)
        {
            if (car.GetComponent<CarScript>() == null)
                continue;

            carList.Add(car);
        }

        ResetMaterial();
        SetCar(0);
    }

    private void OnApplicationQuit()
    {
        ResetMaterial();
    }


    public void SetCar(int carIndex)
    {
        if (carList == null)
        {
            Debug.LogError("Can't set car, car list is not set");
            return;
        }
        if (carIndex < 0 || carIndex >= carList.Count)
        {
            Debug.LogError("Can't set car, car index is out of bounds");
            return;
        }
        GameObject newCar = carList[carIndex];

        if (selectedCar != null)
        {
            Destroy(selectedCar.gameObject);
        }

        selectedCar = Instantiate(newCar, carContainer).GetComponent<CarScript>();
    }

    public void ApplyAttachment(string categoryCode, string attachmentCode)
    {
        //TuningCategory category = car.tu
    }

    public void ApplyMaterial(CarMaterial material)
    {
        if (material == null)
        {
            Debug.LogError("Can't apply paint material, because material parameter is null");
            return;
        }
        car.tuning.selectedMaterial = material;
    }

    public void ApplyMaterialByIndex(int materialIndex)
    {
        if (materialIndex > 0 && materialIndex < car.tuning.carMaterials.Count)
        {
            ApplyMaterial(car.tuning.carMaterials[materialIndex]);
        }
    }

    public void ApplyMaterialByCode(string materialCode)
    {
        if (materialCode == null || materialCode == "")
        {
            Debug.LogWarning("Material code is null");
            return;
        }

        CarMaterial material = car.tuning.carMaterials.Find(x => x.materialCode.Equals(materialCode));
        ApplyMaterial(material);
    }


    private void ResetMaterial()
    {
        if (selectedCar != null && car != null && car.tuning != null && car.tuning.carMaterials.Count > 0)
        {
            Material defaultMaterial = car.tuning.carMaterials[0].material;
            selectedCar.carPaintMaterial.CopyPropertiesFromMaterial(defaultMaterial);
        }
    }

    public void Update()
    {
        if (car == null || car.tuning.selectedMaterial == null || car.tuning.selectedMaterial.material == null)
        {
            return;
        }

        selectedCar.carPaintMaterial.Lerp(selectedCar.carPaintMaterial, car.tuning.selectedMaterial.material, materialChangeSpeed);
    }
}
