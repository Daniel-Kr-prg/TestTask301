using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public UnityEvent mouseDownEvent = new UnityEvent();
    
    public UnityEvent mouseUpEvent = new UnityEvent();
    
    public UnityEvent mouseScrollEvent = new UnityEvent();

    public UnityEvent mouseMoveEvent = new UnityEvent();


    public static InputHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownEvent.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpEvent.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            mouseMoveEvent.Invoke();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mouseScrollEvent.Invoke();
        }
    }
}
