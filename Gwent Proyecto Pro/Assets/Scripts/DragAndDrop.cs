using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 pointerOffset;
    private bool dragging = false;
    private RectTransform canvasRectTransform;
    private bool moveCard = false;
    private Vector3 position;
    public List<string> validPositions = new List<string>();
    public static bool[,] ocupedPosition = new bool[6, 6];

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveCard = true;
        position  = transform.position;
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

    void OnTriggerStay(Collider pos)
    {
        Debug.Log("Hola entre");

        int row = pos.GetComponent<PositionOfCards>().row;
        int col = pos.GetComponent<PositionOfCards>().col;

        if (ocupedPosition[row, col])
        {
            transform.position = position;
        }
        else
        {
            if (validPositions.Contains(pos.tag))
            {
                Debug.Log(row);
                Debug.Log(col);
                Vector3 coordenas = pos.transform.position;
                transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                Debug.Log("Hola estoy");
                //ocupedPosition[row, col] = true;
            }
            else
            {
                transform.position = position;
            }
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        moveCard = false;
    }
}
