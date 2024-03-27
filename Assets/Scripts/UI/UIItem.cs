using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    /// <summary>
    /// Event called on mouse press
    /// </summary>
    public UnityEvent buttonPressEvent = new UnityEvent();

    /// <summary>
    /// Event called on select event in ScrollSnap
    /// </summary>
    public UnityEvent itemSelectEvent = new UnityEvent();

    /// <summary>
    /// Image to be shown on the item
    /// </summary>
    [SerializeField]
    private Image itemImage;

    /// <summary>
    /// Image showing that the item is selected
    /// </summary>
    [SerializeField]
    private GameObject selectedImageObject;

    /// <summary>
    /// The name of the item
    /// </summary>
    [SerializeField]
    private TMP_Text itemText;

    /// <summary>
    /// Connected tuning component
    /// </summary>
    public TuningBase connectedItem;

    /// <summary>
    /// Initialize the UI item
    /// </summary>
    /// <param name="connectedItem">Connected tuning component</param>
    /// <param name="pressCall">Event called on press</param>
    /// <param name="selectCall">Event called on scrollsnap select event</param>
    public void SetItem(TuningBase connectedItem, UnityAction pressCall, UnityAction selectCall)
    {
        if (connectedItem != null)
        {
            SetText(connectedItem.itemName);
            SetLocalizationReference(connectedItem.localStr);

            SetImage(connectedItem.preview);
            this.connectedItem = connectedItem;
        }

        buttonPressEvent.RemoveAllListeners();
        if (pressCall != null)
        {
            buttonPressEvent.AddListener(pressCall);
        }

        itemSelectEvent.RemoveAllListeners();
        if (selectCall != null)
        {
            itemSelectEvent.AddListener(selectCall);
        }
    }

    public void InvokeButtonPress()
    {
        if (buttonPressEvent != null)
        {
            buttonPressEvent.Invoke();
        }
    }

    public void SetText(string text)
    {
        itemText.text = text;
    }

    public void SetLocalizationReference(LocalizedString str)
    {
        LocalizeStringEvent lse = GetComponentInChildren<LocalizeStringEvent>();
        lse.StringReference = new LocalizedString(str.TableReference, str.TableEntryReference);
    }

    public void SetImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetSelected(bool value)
    {
        selectedImageObject.SetActive(value);
    }

    public void UpdateUI()
    {
        if (connectedItem != null)
        {
            SetSelected(connectedItem.isSelected);
        }
    }
}
