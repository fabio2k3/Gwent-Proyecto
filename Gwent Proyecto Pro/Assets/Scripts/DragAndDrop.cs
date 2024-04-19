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
    public static GameObject[,] decoyGameObjects = new GameObject[6, 6];
    public static GameObject[] climateCards = new GameObject[3];
    public static int rowInvocated = 0;

    public GameObject cardInvocated;
    
    private bool originalOrcCardPosition;
    private bool originalWarriorCardPosition;
    public static bool wantToChange;

    #region Effects
    public PlusTwo plusTwo;
    public PlusOne plusOne;
    public MultiplicateTwo multiplicateTwo;
    public MultiplicateThree multiplicateThree;
    public ClimateEffects climateEffects;
    public DecoyEffect decoyEffect;
    public ClearEffect clearEffect;
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

        gameObject.GetComponent<Cards>().originalPosition = transform.localPosition;
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
            int positionClimate = pos.GetComponent<PositionOfCards>().positionOfClimate;

            if(checkClimate.ClimateCards.Contains(gameObject.GetComponent<Cards>().name))
            {
                if (validPositions.Contains(pos.tag))
                {
                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    //gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;
                    gameObject.GetComponent<Cards>().colInvocated = col;

                    gameObject.GetComponent<Cards>().climateInvocated = positionClimate;
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
                    //gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;
                    gameObject.GetComponent<Cards>().colInvocated = col;

                    gameObject.GetComponent<Cards>().climateInvocated = positionClimate;
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
                
                originalWarriorCardPosition = true;

                return;
            }

            int row = pos.GetComponent<PositionOfCards>().row;
            int col = pos.GetComponent<PositionOfCards>().col;
            int positionClimate = pos.GetComponent<PositionOfCards>().positionOfClimate;

            if (checkClimate.ClimateCards.Contains(gameObject.GetComponent<Cards>().name))
            {
                if (validPositions.Contains(pos.tag))
                {

                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);
                    //gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;
                    gameObject.GetComponent<Cards>().colInvocated = col;

                    gameObject.GetComponent<Cards>().climateInvocated = positionClimate;
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
                    //gameObjectsCards[row, col] = gameObject;

                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;
                    gameObject.GetComponent<Cards>().colInvocated = col;

                    gameObject.GetComponent<Cards>().climateInvocated = positionClimate;
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
        if (originalWarriorCardPosition)
            transform.localPosition = originalCanvasPosition;

        int row = gameObject.GetComponent<Cards>().rowInvocated;
        int col = gameObject.GetComponent<Cards>().colInvocated;
        int positionCLimateArray = gameObject.GetComponent<Cards>().climateInvocated;

        if (gameObject.GetComponent<Cards>().name == "Climate1" || gameObject.GetComponent<Cards>().name == "Climate2" || gameObject.GetComponent<Cards>().name == "Climate3" || gameObject.GetComponent<Cards>().name == "Climate4" || gameObject.GetComponent<Cards>().name == "Climate5" || gameObject.GetComponent<Cards>().name == "ClimateW1" || gameObject.GetComponent<Cards>().name == "ClimateW2" || gameObject.GetComponent<Cards>().name == "ClimateW3" || gameObject.GetComponent<Cards>().name == "ClimateW4" || gameObject.GetComponent<Cards>().name == "ClimateW5")
            climateCards[positionCLimateArray] = gameObject; 
        else if(gameObject.GetComponent<Cards>().type == "Decoy")
            decoyGameObjects[row,col] = gameObject;
        else
            gameObjectsCards[row,col] = gameObject;


        #region Activated Effects
        if (gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Increase1" || gameObject.GetComponent<Cards>().name == "Increase2" || gameObject.GetComponent<Cards>().name == "IncreaseW5"))
            plusTwo.enabled = true;

        if(gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Increase5" || gameObject.GetComponent<Cards>().name == "Increase4" || gameObject.GetComponent<Cards>().name == "IncreaseW6" || gameObject.GetComponent<Cards>().name == "IncreaseW4"))
            multiplicateTwo.enabled = true;

        if(gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Increase3" || gameObject.GetComponent<Cards>().name == "IncreaseW2" || gameObject.GetComponent<Cards>().name == "IncreaseW1"))
            plusOne.enabled = true;

        if (gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "IncreaseW3"))
            multiplicateThree.enabled = true;

        if (gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Climate1" || gameObject.GetComponent<Cards>().name == "Climate2" || gameObject.GetComponent<Cards>().name == "Climate3" || gameObject.GetComponent<Cards>().name == "Climate4" || gameObject.GetComponent<Cards>().name == "Climate5" || gameObject.GetComponent<Cards>().name == "ClimateW1" || gameObject.GetComponent<Cards>().name == "ClimateW2" || gameObject.GetComponent<Cards>().name == "ClimateW3" || gameObject.GetComponent<Cards>().name == "ClimateW4" || gameObject.GetComponent<Cards>().name == "ClimateW5"))
            climateEffects.enabled = true;

        if (gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Decoy1" || gameObject.GetComponent<Cards>().name == "Decoy2" || gameObject.GetComponent<Cards>().name == "Decoy3" || gameObject.GetComponent<Cards>().name == "Decoy4" || gameObject.GetComponent<Cards>().name == "Decoy5" || gameObject.GetComponent<Cards>().name == "DecoyW1" || gameObject.GetComponent<Cards>().name == "DecoyW2" || gameObject.GetComponent<Cards>().name == "DecoyW3" || gameObject.GetComponent<Cards>().name == "DecoyW4" || gameObject.GetComponent<Cards>().name == "DecoyW5"))
        {
            decoyEffect.enabled = true;
            Debug.Log("Invocated Decoy");
        } 

        if(gameObject.GetComponent<Cards>().invocated && (gameObject.GetComponent<Cards>().name == "Clear1" || gameObject.GetComponent<Cards>().name == "Clear2" || gameObject.GetComponent<Cards>().name == "Clear3" || gameObject.GetComponent<Cards>().name == "ClearW1" || gameObject.GetComponent<Cards>().name == "ClearW2" || gameObject.GetComponent<Cards>().name == "ClearW3" ))
            clearEffect.enabled = true;
        #endregion

        if (GameFlow.canTakeACard && GameFlow.movement < 2)
            GameFlow.movement++;
        
        GameFlow.whoStart = (GameFlow.whoStart == 0) ? 1 : 0;

        if(GameFlow.whoStart == 0)
            GameFlow.tourWarriors = true;
        if (GameFlow.whoStart == 1)
            GameFlow.tourOrcs = true;
    }
}
