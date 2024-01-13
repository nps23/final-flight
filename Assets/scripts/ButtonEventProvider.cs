using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonEventProvider : MonoBehaviour, IPointerEnterHandler
{
    // callbacks
    [SerializeField] UnityEvent onMouseEnter;
    
    
    // not really sure what to do with the arg here
    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseEnter?.Invoke();
    }
}
