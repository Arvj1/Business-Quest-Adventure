using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectMatchingGame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public CH2ST3Manager manager;
    public int matchID; // Set this in the inspector or dynamically
    private LineRenderer lineRenderer;
    private GameObject startObject;
    private bool isDrawing = false;

    void Start()
    {
        // Initialize LineRenderer
        startObject = gameObject;

        GameObject lineObject = new GameObject("Line");
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;

        // Set material for the line (optional)
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(193, 225, 193, 200);
        lineRenderer.endColor = new Color(193, 225, 193, 200);
        lineRenderer.sortingOrder = 25;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startObject = gameObject; // Save the clicked object
        isDrawing = true;
        Vector3 startPos = startObject.transform.position;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, startPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrawing)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
            lineRenderer.SetPosition(1, currentPos);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrawing = false;

        // Detect object under the pointer
        GameObject endObject = eventData.pointerCurrentRaycast.gameObject;
        
        if (endObject != null)
        {
            ObjectMatchForm endScript = endObject.GetComponent<ObjectMatchForm>();
            // Check matchID
            if (endScript != null && matchID == endScript.Get_ID())
            {
                lineRenderer.SetPosition(1, endObject.transform.position); // Finalize line
                manager.count++;
                if(manager.count == 5)
                {
                    manager.OnGameOver.Invoke();
                }
                Debug.Log("Matched!");
            }
            else
            {
                ClearLine();
                Debug.Log("No match!");
            }
        }
        else
        {
            ClearLine();
        }
    }

    private void ClearLine()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = 2;
    }
}
