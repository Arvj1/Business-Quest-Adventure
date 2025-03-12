
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage2DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initalPosition;
    private Vector3 offset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Assuming the main camera is the one rendering the Canvas
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initalPosition = transform.position;
        // Calculate the offset between the object's position and the pointer position
        offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, mainCamera.nearClipPlane));

        // Disable raycasts on the object being dragged
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert the pointer's position to world space and apply the offset
        Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y, mainCamera.nearClipPlane);
        transform.position = mainCamera.ScreenToWorldPoint(screenPoint) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the position to the initial position if needed
        transform.position = initalPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
