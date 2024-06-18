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
    private bool moveCard = false; // verificar que se mueve la carta
    private Vector3 position;
    private Vector3 originalCanvasPosition; // Posicion Original de las cartas
    public List<string> validPositions = new List<string>(); // Posiciones donde pueden estar las cartas

    public static bool invocated;
    public static GameObject[,] gameObjectsCards = new GameObject[6, 6]; // Matriz GameObject Cartas
    public static GameObject[,] decoyGameObjects = new GameObject[6, 6]; // Matriz para las cartas Señuelo
    public static GameObject[] climateCards = new GameObject[3]; // Array cartas Clima
    public static int rowInvocated = 0; 

    public GameObject cardInvocated;
    
    /* Verificar cual de los dos Jugadores esta jugando
      para que el contrario no invoque cartas */
    private bool originalOrcCardPosition;
    private bool originalWarriorCardPosition;

    #region Effects
    // Para invocar el efecto de las carta de Aumento
    public PlusTwo plusTwo;  
    public PlusOne plusOne;
    public MultiplicateTwo multiplicateTwo;
    public MultiplicateThree multiplicateThree;

    // Para invocar efecto cartas clima
    public ClimateEffects climateEffects;

    // Para invocar efecto cartas Señuelo
    public DecoyEffect decoyEffect;
     
    // Para invocar efecto cartas Despeje
    public ClearEffect clearEffect;
    #endregion


    void Start()
    {
        // Inicializar la referencia al RectTransform del canvas que contiene este objeto
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        originalCanvasPosition = transform.localPosition; // tomar la posicion dentro del canvas
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveCard = true; // cuando se da click sobre la carta indica que se va amover
        position = transform.position; // guardar la posicion actual

        // Se calcula el desplazamiento del cursor en relacion con el objeto
        Vector3 pointerPos = eventData.pointerCurrentRaycast.screenPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, pointerPos, eventData.enterEventCamera, out pointerOffset);
        pointerOffset -= transform.position;

        dragging = true; // se activa el arrastre

        // se guarda en la propiedad originalPosition de Cards la posicion original de la carta
        gameObject.GetComponent<Cards>().originalPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // si se esta moviendo el objeto, se actualiza la posicion en funcion del cursor
        if (dragging)
        {
            Vector3 pointerPos = eventData.pointerCurrentRaycast.screenPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, pointerPos, eventData.enterEventCamera, out Vector3 currentPointerOffset);
            transform.position = currentPointerOffset - pointerOffset;
        }
    }

    void OnTriggerStay(Collider pos)
    {
        GameFlow checkClimate = new GameFlow(); // Instancia de la clase GameFlow

        if(GameFlow.avoidCampOrc)
        {
            if (pos.CompareTag("WCamp")) // Verifica si colisiona con el campo de los Warriors
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), pos, true); // Ignorar la colision

                return;
            }

            if (pos.CompareTag("OrcCamp")) // Verifica si colisiona con el campo de los Warriors
            {          
                // Restaura la posición original del objeto
                originalOrcCardPosition = true;

                return;
            }

            int row = pos.GetComponent<PositionOfCards>().row; // asignarle valor a la propiedad row (fila) de Cards
            int col = pos.GetComponent<PositionOfCards>().col; // asignarle valor a la propiedad col (columna) de Cards
            int positionClimate = pos.GetComponent<PositionOfCards>().positionOfClimate; // asignarle valor la propiedad positionOfClimate (posicion dentro del array climateCards) de Carrds

            if(checkClimate.ClimateCards.Contains(gameObject.GetComponent<Cards>().name)) // verificar que estas tomando una carta clima
            {
                if (validPositions.Contains(pos.tag)) // verificar si puede ocupar dicha posicion
                {
                    // cambiar la poscion con el gameobject que colisiono
                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);

                    // actualizar las propiedades de la carta
                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().climateInvocated = positionClimate;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
            else // caso que no sea clima
            {
                if (validPositions.Contains(pos.tag)) // verificar si puede ocupar dicha posicion
                {
                    // cambiar la poscion con el gameobject que colisiono
                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);

                    // actualizar las propiedades de las cartas
                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;
                    gameObject.GetComponent<Cards>().colInvocated = col;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
            
        }

        if (GameFlow.avoidCampWarrior)
        {
            if (pos.CompareTag("OrcCamp")) // Verifica si colisiona con el campo de los Warriors
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), pos, true); // Ignorar la colision

                return;
            }

            if (pos.CompareTag("WCamp")) // Verifica si colisiona con el campo de los Warriors
            {
                
                originalWarriorCardPosition = true; // Restaura la posición original del objeto

                return;
            }

            int row = pos.GetComponent<PositionOfCards>().row; // asignarle valor a la propiedad row (fila) de Cards
            int col = pos.GetComponent<PositionOfCards>().col; // asignarle valor a la propiedad col (columna) de Cards
            int positionClimate = pos.GetComponent<PositionOfCards>().positionOfClimate; // asignarle valor la propiedad positionOfClimate (posicion dentro del array climateCards) de Carrds

            if (checkClimate.ClimateCards.Contains(gameObject.GetComponent<Cards>().name)) // verificar que estas tomando una carta clima
            {
                if (validPositions.Contains(pos.tag)) // verificar si puede ocupar dicha posicion
                {
                    // cambiar la poscion con el gameobject que colisiono
                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);

                    // actualizar las propiedades de la carta
                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().climateInvocated = positionClimate;
                }
                else
                {
                    transform.localPosition = originalCanvasPosition;
                }
            }
            else // caso que no sea clima
            {
                if (validPositions.Contains(pos.tag)) // verificar si puede ocupar dicha posicion
                {
                    // cambiar la poscion con el gameobject que colisiono
                    Vector3 coordenas = pos.transform.position;
                    transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);

                    // actualizar las propiedades de las cartas
                    gameObject.GetComponent<Cards>().invocated = true;
                    gameObject.GetComponent<Cards>().rowInvocated = row;
                    gameObject.GetComponent<Cards>().colInvocated = col;
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
