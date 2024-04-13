using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 pointerOffset;
    private bool dragging = false;
    private RectTransform canvasRectTransform;
    private bool moveCard = false;
    private Vector3 position;
    private Vector3 originalCanvasPosition;
    public List<string> validPositions = new List<string>();

    public static bool invocated;
    public static GameObject[,] gameObjectsCards = new GameObject[6, 6];
    public static int rowInvocated = 0;

    public GameObject cardInvocated;
    
    private bool originalOrcCardPosition;

    #region Effects
    public PlusTwo plusTwo;
    public PlusOne plusOne;
    public MultiplicateTwo multiplicateTwo;
    public MultiplicateThree multiplicateThree;
    #endregion


    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        originalCanvasPosition = transform.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveCard = true;
        position = transform.position;
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
        GameFlow checkClimate = new GameFlow();

        if(GameFlow.avoidCampOrc)
        {
            if (pos.CompareTag("WCamp")) // Verifica si colisiona con un WCamp
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), pos, true);

                return;
            }

            if (pos.CompareTag("OrcCamp")) // Verifica si colisiona con un OrcCamp
            {          
                // Restaura la posición original del objeto
                originalOrcCardPosition = true;

                return;
            }

            int row = pos.GetComponent<PositionOfCards>().row;
            int col = pos.GetComponent<PositionOfCards>().col;

            if(checkClimate.ClimateCards.Contains(gameObject.GetComponent<Cards>().name))
            {
                if (validPositions.Contains(pos.tag))
                {

                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;

                    //if (gameObjectsCards[row, col] is null)
                    //{
                    //    Vector3 coordenas = pos.transform.position;
                    //    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    //    gameObjectsCards[row, col] = gameObject;

                    //    gameObject.GetComponent<Cards>().invocated = true;
                    //    gameObject.GetComponent<Cards>().rowInvocated = row;
                    //}
                    //else
                    //    transform.localPosition = originalCanvasPosition;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
            else
            {
                if (validPositions.Contains(pos.tag))
                {

                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;

                    //if (gameObjectsCards[row, col] is null)
                    //{
                    //    Vector3 coordenas = pos.transform.position;
                    //    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    //    gameObjectsCards[row, col] = gameObject;

                    //    gameObject.GetComponent<Cards>().invocated = true;
                    //    gameObject.GetComponent<Cards>().rowInvocated = row;
                    //}
                    //else
                    //    transform.localPosition = originalCanvasPosition;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
            
        }

        if (GameFlow.avoidCampWarrior)
        {
            if (pos.CompareTag("OrcCamp")) 
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), pos, true);

                return;
            }

            if (pos.CompareTag("WCamp")) 
            {
                
                originalOrcCardPosition = true;

                return;
            }

            int row = pos.GetComponent<PositionOfCards>().row;
            int col = pos.GetComponent<PositionOfCards>().col;

            if (checkClimate.ClimateCards.Contains(gameObject.GetComponent<Cards>().name))
            {
                if (validPositions.Contains(pos.tag))
                {

                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;

                    //if (gameObjectsCards[row, col] is null)
                    //{
                    //    Vector3 coordenas = pos.transform.position;
                    //    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    //    gameObjectsCards[row, col] = gameObject;

                    //    gameObject.GetComponent<Cards>().invocated = true;
                    //    gameObject.GetComponent<Cards>().rowInvocated = row;
                    //}
                    //else
                    //    transform.localPosition = originalCanvasPosition;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
            else
            {
                if (validPositions.Contains(pos.tag))
                {

                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;

                    //if (gameObjectsCards[row, col] is null)
                    //{
                    //    Vector3 coordenas = pos.transform.position;
                    //    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    //    gameObjectsCards[row, col] = gameObject;

                    //    gameObject.GetComponent<Cards>().invocated = true;
                    //    gameObject.GetComponent<Cards>().rowInvocated = row;
                    //}
                    //else
                    //    transform.localPosition = originalCanvasPosition;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
        }
        
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        moveCard = false;

        if (originalOrcCardPosition)
            transform.localPosition = originalCanvasPosition;


        invocated = true;

        Debug.Log(invocated);

        if (gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Increase1" || gameObject.GetComponent<Cards>().name == "Increase2" || gameObject.GetComponent<Cards>().name == "IncreaseW5"))
            plusTwo.enabled = true;

        if(gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Increase5" || gameObject.GetComponent<Cards>().name == "Increase4" || gameObject.GetComponent<Cards>().name == "IncreaseW6" || gameObject.GetComponent<Cards>().name == "IncreaseW4"))
            multiplicateTwo.enabled = true;

        if(gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Increase3" || gameObject.GetComponent<Cards>().name == "IncreaseW2" || gameObject.GetComponent<Cards>().name == "IncreaseW1"))
            plusOne.enabled = true;

        if (gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "IncreaseW3"))
            multiplicateThree.enabled = true;
    }
}
