using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 pointerOffset;
    private bool dragging = false;
    private RectTransform canvasRectTransform;
    private bool moveCard = false;
    private Vector3 position;
    public List<string> validPositions = new List<string>();
    public static bool[,] ocupedPosition = new bool[6, 6];
    public GameObject[,] gameObjectsCards = new GameObject[6, 6];

    public GameObject cardInvocated;


    private bool ocuped = false;
    private int rowOcuped;
    private int colOcuped;
   
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
                cardInvocated = gameObject;
                Vector3 coordenas = pos.transform.position;
                transform.position = new Vector3(coordenas.x, coordenas.y, coordenas.z);

                ocuped = true;
                rowOcuped = row;
                colOcuped = col;

                gameObjectsCards[row, col] = cardInvocated;

                Effects(cardInvocated, row, ocupedPosition, gameObjectsCards);
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

        if (ocuped)
        {
            ocupedPosition[rowOcuped, colOcuped] = true;
            Debug.Log(ocupedPosition[rowOcuped, colOcuped]);
        }
    }

    public void Effects(GameObject cardWithEffect, int row, bool[,] cardInvocatedBool, GameObject[,] cardInvocated)
    {
        Cards card = cardWithEffect.GetComponent<Cards>();

        #region Increase
        if(card.name == "Increase4" || card.name == "Increase5" || card.name == "IncreaseW4" || card.name == "IncreaseW6")
        {
            for(int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row,col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack *= 2;
            }
        }
        else if(card.name == "Increase2" || card.name == "IncreaseW5")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack += 2;
            }
        }
        else if(card.name == "Increase3")
        {
            if(row == 2)
            {
                for (int col = 0; col < 6; col++)
                {
                    if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                        cardInvocated[row, col].GetComponent<Cards>().attack += 2;
                }
            }
            else
            {
                for (int col = 0; col < 6; col++)
                {
                    if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                        cardInvocated[row, col].GetComponent<Cards>().attack += 1;
                }
            }
        }
        else if(card.name == "IncreaseW1")
        {
            if (row == 4)
            {
                for (int col = 0; col < 6; col++)
                {
                    if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                        cardInvocated[row, col].GetComponent<Cards>().attack += 3;
                }
            }
            else
            {
                for (int col = 0; col < 6; col++)
                {
                    if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                        cardInvocated[row, col].GetComponent<Cards>().attack += 1;
                }
            }
        }
        else if( card.name == "IncreaseW3")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack *= 3;
            }
        }
        else if(card.name == "Increase1")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && (cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverOrc1" || cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverOrc2" || cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverOrc3") && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack += 5;

                else if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack += 2;
            }
        }
        else if (card.name == "Increase6")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && (cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverOrc1" || cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverOrc2" || cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverOrc3") && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack += 2;

                else if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack *= 3;
            }
        }
        else if(card.name == "IncreaseW2")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && (cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverW1" || cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverW2" || cardInvocated[row, col].GetComponent<Cards>().name == "UnitSilverW3") && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack += 2;

                else if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack += 1;
            }
        }
        #endregion

        #region Climate
        else if(card.name == "Increase3" || card.name == "IncreaseW4" || card.name == "IncreaseW5")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack -= 2;

                if (cardInvocated[row, col].GetComponent<Cards>().attack < 0)
                    cardInvocated[row, col].GetComponent<Cards>().attack = 0;
            }
        }

        else if( card.name == "Increase4"  || card.name == "IncreaseW1")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack -= 3;

                if (cardInvocated[row, col].GetComponent<Cards>().attack < 0)
                    cardInvocated[row, col].GetComponent<Cards>().attack = 0;
            }
        }

        else if(card.name == "Increase1")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[0, col] && cardInvocated[0, col].GetComponent<Cards>().type == "Unit" && cardInvocated[0, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[0, col].GetComponent<Cards>().attack = 0;

                if (cardInvocatedBool[1, col] && cardInvocated[1, col].GetComponent<Cards>().type == "Unit" && cardInvocated[1, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[1, col].GetComponent<Cards>().attack = 0;

                if (cardInvocatedBool[4, col] && cardInvocated[4, col].GetComponent<Cards>().type == "Unit" && cardInvocated[4, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[4, col].GetComponent<Cards>().attack = 0;

                if (cardInvocatedBool[5, col] && cardInvocated[5, col].GetComponent<Cards>().type == "Unit" && cardInvocated[5, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[5, col].GetComponent<Cards>().attack = 0;
            }
        }

        else if (card.name == "IncreaseW3")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[0, col] && cardInvocated[0, col].GetComponent<Cards>().type == "Unit" && cardInvocated[0, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[0, col].GetComponent<Cards>().attack = 0;
                
                if (cardInvocatedBool[2, col] && cardInvocated[2, col].GetComponent<Cards>().type == "Unit" && cardInvocated[2, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[2, col].GetComponent<Cards>().attack = 0;

                if (cardInvocatedBool[3, col] && cardInvocated[3, col].GetComponent<Cards>().type == "Unit" && cardInvocated[4, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[3, col].GetComponent<Cards>().attack = 0;

                if (cardInvocatedBool[5, col] && cardInvocated[5, col].GetComponent<Cards>().type == "Unit" && cardInvocated[5, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[5, col].GetComponent<Cards>().attack = 0;
            }
        }

        else if(card.name == "Increase5" || card.name == "IncreaseW2" || card.name == "Increase2")
        {
            for (int col = 0; col < 6; col++)
            {
                if (cardInvocatedBool[row, col] && cardInvocated[row, col].GetComponent<Cards>().type == "Unit" && cardInvocated[row, col].GetComponent<Cards>().name != "UnitGoldOrc")
                    cardInvocated[row, col].GetComponent<Cards>().attack = 0;
            }
        }
        #endregion
    }
}
