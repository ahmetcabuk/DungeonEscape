using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionButton : Singleton<InteractionButton>, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonDown;
    public UnityEvent OnInteraction = new UnityEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonDown = true;
        OnInteraction.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonDown = false;
    }
}
