using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage4DropLocation : MonoBehaviour, IDropHandler
{
    [SerializeField] List<GameObject> pollution; 
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;

        int n = pollution.Count;
        for(int i=0; i<n; i++)
        {
            Destroy(pollution[i]);
        }

        Destroy(obj, 0.1f);

    }
}
