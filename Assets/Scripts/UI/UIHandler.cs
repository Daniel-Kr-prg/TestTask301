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

    [SerializeField]
    private Button prevButton;

    [SerializeField]
    private Button nextButton;

    private TuningCategory selectedCategory;

    private List<UnityAction> MenuStack = new List<UnityAction>();

    public void CreateCarSelectionUI()
    {
        List<Car> cars = configurator.GetCarList().Select(c => c.GetComponent<CarObject>().GetCar()).ToList();

        ResetScrollSnap();

        for (int i = 0; i < cars.Count; i++)
        {
            Car car = cars[i];

            CreateUIItem(car.description, null, () => 
            {
                configurator.SetCar(cars.IndexOf(car));
                CreateListUI(car);
                UpdateList();

                MenuStack.Add(() =>
                {
                    CreateCarSelectionUI();
                });
            });
        }

        backButton.enabled = false;
        HandleMoveButtons();
    }

    public void CreateListUI(TuningList list)
    {
        ResetScrollSnap();

        foreach (var categoryItem in list.categories)
        {
            CreateUIItem(categoryItem.description, null, () =>
            {
                selectedCategory = categoryItem;
                configurator.SelectCategory(categoryItem);
                CreateTuningCategoryUI(categoryItem);
                UpdateList();

                MenuStack.Add(() =>
                {
                    CreateListUI(list);
                });
            });
        }

        backButton.enabled = true;
        HandleMoveButtons();
    }

    public void CreateTuningCategoryUI(TuningCategory category)
    {
        ResetScrollSnap();

        foreach (var item in category.tuningItems)
        {
            (item as ITuningElement).GetElementType(this);
        }
        foreach (var categoryItem in category.categories)
        {
            CreateUIItem(categoryItem.description, null, () =>
            {
                selectedCategory = categoryItem;
                configurator.SelectCategory(categoryItem);
                CreateTuningCategoryUI(categoryItem);
                UpdateList();

                MenuStack.Add(() =>
                {
                    CreateTuningCategoryUI(category);
                });
            });
        }

        backButton.enabled = true;
        HandleMoveButtons();
    }

    //public void CreateTuningElementsUI(List<TuningAppliaple> tuningItems)
    //{
    //    ResetScrollSnap();
        
    //    foreach (var item in tuningItems)
    //    {
    //        (item as ITuningElement).GetElementType(this);
    //    }

    //    backButton.enabled = true;
    //    HandleMoveButtons();
    //}

    private void ResetScrollSnap()
    {
        while (_scrollSnap.Content.childCount > 0)
        {
            _scrollSnap.RemoveFromFront();
        }
    }

    private void HandleMoveButtons()
    {
        if (prevButton == null || nextButton == null)
            return;

        prevButton.enabled = _scrollSnap.Content.childCount > 0;
        nextButton.enabled = _scrollSnap.Content.childCount > 0;
    }

    private void CreateUIItem(Description config, TuningBase connectedItem, UnityAction call)
    {
        _scrollSnap.AddToBack(UIItemPrefab);

        UIItem item = _scrollSnap.Content.GetChild(_scrollSnap.Content.childCount - 1).GetComponent<UIItem>();
        
        item.SetText(config.name);
        item.SetImage(config.preview);

        item.connectedItem = connectedItem;

        item.buttonPressEvent.AddListener(call);
    }

    public void SelectComponent(CarConfigurator configurator, TuningAppliaple item)
    {
        configurator.ApplyTuning(item);
    }

    public void PressBackButton()
    {
        if (MenuStack.Count == 0)
            return;

        UnityAction action = MenuStack.Last();
        MenuStack.Remove(action);

        action.Invoke();
    }

    private void UpdateList()
    {
        foreach (Transform t in _scrollSnap.Content)
        {
            UIItem uiItem = t.GetComponent<UIItem>();
            if (uiItem != null)
            {
                uiItem.UpdateUI();
            }
        }
    }

    public void HandleElementType(TuningComponent tuningComponent)
    {
        CreateUIItem(tuningComponent.description, tuningComponent, () => 
        { 
            SelectComponent(configurator, tuningComponent);
            UpdateList();
        });
    }

    public void HandleElementType(CarMaterial carMaterial)
    {
        CreateUIItem(carMaterial.description, carMaterial, () => 
        { 
            SelectComponent(configurator, carMaterial);
            UpdateList();
        });
    }
}