using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initalPosition;
    public UnityEvent OnBegin,OnDraging,OnDrop;
    public void OnBeginDrag(PointerEventData eventData)
    {
        initalPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        OnBegin.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
        OnDraging.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initalPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        OnDrop.Invoke();    
    }
}
