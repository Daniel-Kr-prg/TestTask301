using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.VersionControl;
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

    /// <summary>
    /// Stack of calls used for Back Button
    /// </summary>
    private List<UnityAction> MenuStack = new List<UnityAction>();

    public void Start()
    {
        if (configurator == null)
        {
            configurator = FindObjectOfType<CarConfigurator>();
        }
    }

    public void CreateCarSelectionUI()
    {
        List<Car> cars = configurator.GetCarList().Select(c => c.GetComponent<Car>()).ToList();

        ResetScrollSnap();
        CameraHandler.instance.SetTargetToDefault();

        // Set current car to the default one
        configurator.SetCar(0);

        // Create list of UI Items for cars
        for (int i = 0; i < cars.Count; i++)
        {
            Car car = cars[i];

            CreateElement(
                car.GetCategoryData()
                ,() => { CarSelectionPress(cars.IndexOf(car)); }
                ,() => { CarSelectionSelect(cars.IndexOf(car)); }
            );
        }

        backButton.enabled = false;
        HandleMoveButtons();
    }

    public void CreateListUI(TuningCategory list)
    {
        ResetScrollSnap();
        CameraHandler.instance.SetTarget(list.GetCameraTarget());

        // If category has appliable tuning items, then we show them first
        if (list.GetCategoryData() != null) 
        {
            foreach (var item in list.GetCategoryData().tuningItems)
            {
                if (item.isSelected)
                    _scrollSnap.GoToPanel(list.GetCategoryData().tuningItems.IndexOf(item));

                CreateElement(
                    item
                    ,() => { ItemSelectionPress(list.GetCategoryData().tuningItems.IndexOf(item)); }
                    ,() => { ItemSelectionPress(list.GetCategoryData().tuningItems.IndexOf(item)); }
                );
            }
        }

        // show all inner categories in current category
        foreach (var categoryItem in list.GetChildCategories())
        {
            CreateElement(categoryItem.GetCategoryData()
                ,() => { CategorySelectionPress(list, categoryItem); }
                ,null
            );
        }

        backButton.enabled = true;
        HandleMoveButtons();
    }

    /// <summary>
    /// Create UI Item
    /// </summary>
    private void CreateElement(TuningBase connectedItem, UnityAction pressCall, UnityAction selectCall)
    {
        _scrollSnap.AddToBack(UIItemPrefab);

        UIItem item = _scrollSnap.Content.GetChild(_scrollSnap.Content.childCount - 1).GetComponent<UIItem>();

        item.SetItem(connectedItem, pressCall, selectCall);
    }

    /// <summary>
    /// Get call from MenuStack and invoke it on Back Button press
    /// </summary>
    public void PressBackButton()
    {
        if (MenuStack.Count == 0)
            return;

        UnityAction action = MenuStack.Last();
        MenuStack.Remove(action);

        action.Invoke();
    }

    /// <summary>
    /// Update UI Items
    /// </summary>
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

    /// <summary>
    /// Clear scroll snap content
    /// </summary>
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

    /// <summary>
    /// ScrollSnap select event handler
    /// </summary>
    public void ScrollViewSelectItem(int newVal, int oldVal)
    {
        UIItem item = _scrollSnap.Content.GetChild(newVal).GetComponent<UIItem>();
        if (item != null && item.connectedItem != null)
        {
            item.itemSelectEvent.Invoke();
        }
    }

    /// <summary>
    /// Action called on UI Item press when selecting a car
    /// </summary>
    private void CarSelectionPress(int index)
    {
        configurator.SetCar(index);
        CreateListUI(configurator.GetSelectedCar());
        UpdateList();

        MenuStack.Add(() =>
        {
            CreateCarSelectionUI();
        });
    }

    /// <summary>
    /// Action called on UI Item select when selecting a car
    /// </summary>
    /// <param name="index"></param>
    private void CarSelectionSelect(int index)
    {
        configurator.SetCar(index);
    }

    /// <summary>
    /// Action called on UI Item press when selecting a category
    /// </summary>
    private void CategorySelectionPress(TuningCategory parentList, TuningCategory currentList)
    {
        configurator.SelectCategory(currentList);
        CreateListUI(currentList);
        UpdateList();

        MenuStack.Add(() =>
        {
            CreateListUI(parentList);
        });
    }

    /// <summary>
    /// Action called on UI Item press when selection an appliable item
    /// </summary>
    private void ItemSelectionPress(int index)
    {
        _scrollSnap.GoToPanel(index);
        configurator.ApplyTuning(index);
        UpdateList();
    }
}