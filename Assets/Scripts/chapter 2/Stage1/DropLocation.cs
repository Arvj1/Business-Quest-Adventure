using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropLocation : MonoBehaviour, IDropHandler
{
    public UnityEvent<GameObject,GameObject> OnDroped;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        OnDroped.Invoke(obj,gameObject);
    }
}
