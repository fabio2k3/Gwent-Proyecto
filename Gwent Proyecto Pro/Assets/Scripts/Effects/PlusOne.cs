using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Cards>().name == "Increase3")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (gameObject.GetComponent<Cards>().rowInvocated == 2)
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                        {
                            currentCard.attack += 2;
                        }
                    }
                    else
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                        {
                            currentCard.attack += 1;
                        }
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "IncreaseW1")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (gameObject.GetComponent<Cards>().rowInvocated == 4)
                    {
                        if (currentCard.type == "Unit")
                        {
                            currentCard.attack += 3;
                        }
                    }
                    else
                    {
                        if (currentCard.type == "Unit")
                        {
                            currentCard.attack += 1;
                        }
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "IncreaseW2")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "UnitSilverW1"|| currentCard.name == "UnitSilverW2" || currentCard.name == "UnitSilverW3")
                    {
                        currentCard.attack += 2;   
                    }
                    else
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                        {
                            currentCard.attack += 1;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
