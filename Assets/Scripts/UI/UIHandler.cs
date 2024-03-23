using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Rendering.FilterWindow;

public class UIHandler : MonoBehaviour, ITuningElementsHandler
{
    [SerializeField]
    private SimpleScrollSnap _scrollSnap;

    [SerializeField]
    private GameObject UIItemPrefab;

    [SerializeField]
    private CarConfigurator configurator;

    private UnityEvent backButtonEvent = new UnityEvent();

    [SerializeField]
    private Button backButton;

    private List<List<TuningBase>> ConfigStack = new List<List<TuningBase>>();

    public void CreateCarSelectionUI()
    {
        List<Car> cars = configurator.GetCarList().Select(c => c.GetComponent<CarRoot>().GetCar()).ToList();

        ResetScrollSnap();

        for (int i = 0; i < cars.Count; i++)
        {
            CreateUIItem(cars[i], () => 
            {
                configurator.SetCar(i);
                CreateTuningElementsUI(cars[i].elements);
            });
        }
        backButton.enabled = false;
        backButtonEvent.RemoveAllListeners();
    }

    public void CreateTuningElementsUI(List<TuningBase> elements)
    {
        ResetScrollSnap();
        
        foreach (var element in elements)
        {
            element.GetElementType(this);
        }

        backButton.enabled = true;
    }

    private void ResetScrollSnap()
    {
        for (int i = 0; i < _scrollSnap.Content.childCount; i++)
        {
            _scrollSnap.RemoveFromFront();
        }
    }

    private void CreateUIItem(TuningBase config, UnityAction call)
    {
        UIItem item = UIItemPrefab.GetComponent<UIItem>();

        item.SetText(config.description.name);
        item.SetImage(config.description.preview);
        item.SetSelected(config.isSelected);

        item.buttonPressEvent.RemoveAllListeners();
        item.buttonPressEvent.AddListener(call);

        _scrollSnap.AddToBack(UIItemPrefab);
    }

    public void SelectComponent(CarConfigurator configurator, TuningAppliaple component)
    {
        component.ApplyTuning(configurator);
    }

    public void PressBackButton()
    {
        if (ConfigStack.Count == 0)
            return;
        else if (ConfigStack.Count == 1)
        {
            CreateCarSelectionUI();
        }
        else
        {
            List<TuningBase> categories = ConfigStack.Last();
            ConfigStack.Remove(categories);
            CreateTuningElementsUI(categories);
        }
    }

    public void HandleElementType(TuningComponent tuningComponent)
    {
        CreateUIItem(tuningComponent, () => 
        { 
            SelectComponent(configurator, tuningComponent); 
        });
    }

    public void HandleElementType(CarMaterial carMaterial)
    {
        CreateUIItem(carMaterial, () => 
        { 
            SelectComponent(configurator, carMaterial); 
        });
    }

    public void HandleElementType(TuningCategory tuningCategory)
    {
        CreateUIItem(tuningCategory, () =>
        {
            configurator.SelectCategory(tuningCategory);
            CreateTuningElementsUI(tuningCategory.elements);
        });
    }
}