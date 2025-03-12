using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage2DropLocation : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        if(obj.tag == tag)
        {
            GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
            

            Destroy(obj, 0.1f);
        }
        else
        {
            Debug.Log("Incorrect Answer");
        }
    }
}
