using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage4DragNDropWater : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        initalPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;

        if(eventData.pointerCurrentRaycast.gameObject.tag == "Garbage")
        {
            Destroy(eventData.pointerCurrentRaycast.gameObject);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initalPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
