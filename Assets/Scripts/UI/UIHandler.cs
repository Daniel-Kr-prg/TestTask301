using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private SimpleScrollSnap _scrollSnap;

    [SerializeField]
    private GameObject UIItemPrefab;

    [SerializeField]
    private CarConfigurator configurator;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Button prevButton;

    [SerializeField]
    private Button nextButton;

    private List<UnityAction> MenuStack = new List<UnityAction>();

    public void CreateCarSelectionUI()
    {
        List<Car> cars = configurator.GetCarList().Select(c => c.GetComponent<CarObject>().GetCar()).ToList();

        ResetScrollSnap();
        CameraHandler.instance.SetTargetToDefault();

        _scrollSnap.OnPanelCentered.RemoveAllListeners();
        _scrollSnap.OnPanelCentered.AddListener(ScrollViewSelectCar);

        configurator.SetCar(0);

        for (int i = 0; i < cars.Count; i++)
        {
            Car car = cars[i];

            CreateElement(car, () => 
            {
                configurator.SetCar(cars.IndexOf(car));
                CreateListUI(configurator.GetSelectedCar().GetCar());
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
        CameraHandler.instance.SetTargetToDefault();

        _scrollSnap.OnPanelCentered.RemoveAllListeners();
        _scrollSnap.OnPanelCentered.AddListener(ScrollViewSelectItem);

        foreach (var categoryItem in list.categories)
        {
            CreateElement(categoryItem, () =>
            {
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
        CameraHandler.instance.SetTarget(category.GetAttachmentPoint()?.GetCameraTarget());

        _scrollSnap.OnPanelCentered.RemoveAllListeners();
        _scrollSnap.OnPanelCentered.AddListener(ScrollViewSelectItem);

        foreach (var item in category.tuningItems)
        {
            if (item.isSelected)
                _scrollSnap.GoToPanel(category.tuningItems.IndexOf(item));

            CreateElement(item, () =>
            {
                _scrollSnap.GoToPanel(category.tuningItems.IndexOf(item));
                SelectComponent(configurator, item);
                UpdateList();
            });
        }
        foreach (var categoryItem in category.categories)
        {
            CreateElement(categoryItem, () =>
            {
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

    public void ScrollViewSelectCar(int newVal, int oldVal)
    {
        configurator.SetCar(newVal);
    }

    public void ScrollViewSelectItem(int newVal, int oldVal)
    {
        UIItem item = _scrollSnap.Content.GetChild(newVal).GetComponent<UIItem>();
        if (item != null && item.connectedItem != null)
        {
            item.buttonPressEvent.Invoke();
        }
    }

    private void CreateElement(TuningBase connectedItem, UnityAction call)
    {
        _scrollSnap.AddToBack(UIItemPrefab);

        UIItem item = _scrollSnap.Content.GetChild(_scrollSnap.Content.childCount - 1).GetComponent<UIItem>();
        
        item.SetText(connectedItem.itemName);
        item.SetImage(connectedItem.preview);

        item.connectedItem = connectedItem;

        item.buttonPressEvent.AddListener(call);
    }

    private void CreateElement(TuningList listItem, UnityAction call)
    {
        _scrollSnap.AddToBack(UIItemPrefab);

        UIItem item = _scrollSnap.Content.GetChild(_scrollSnap.Content.childCount - 1).GetComponent<UIItem>();

        item.SetText(listItem.name);
        item.SetImage(listItem.preview);

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
}