using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 pointerOffset;
    private bool dragging = false;
    private RectTransform canvasRectTransform;

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 pointerPos = eventData.pointerCurrentRaycast.screenPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, pointerPos, eventData.enterEventCamera, out pointerOffset);
        pointerOffset -= transform.position;
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            Vector3 pointerPos = eventData.pointerCurrentRaycast.screenPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, pointerPos, eventData.enterEventCamera, out Vector3 currentPointerOffset);
            transform.position = currentPointerOffset - pointerOffset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}
