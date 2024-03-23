using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public UnityEvent buttonPressEvent;

    public Image itemImage;

    public GameObject selectedImageObject;

    public TMP_Text itemText;

    public void InvokeButtonPress()
    {
        if (buttonPressEvent != null)
        {
            buttonPressEvent.Invoke();
        }
    }

    public void SetText(string newText)
    {
        itemText.text = newText;
    }

    public void SetImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetSelected(bool value)
    {
        selectedImageObject.SetActive(value);
    }
}
