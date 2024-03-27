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
    public UnityEvent buttonPressEvent;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private GameObject selectedImageObject;

    [SerializeField]
    private TMP_Text itemText;

    public TuningBase connectedItem;


    public void SetItem(Sprite image, string text, LocalizedString localStr, TuningBase connectedItem, UnityAction call)
    {
        SetText(text);
        SetLocalizationReference(localStr);

        SetImage(image);
        this.connectedItem = connectedItem;

        buttonPressEvent.RemoveAllListeners();
        buttonPressEvent.AddListener(call);
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
