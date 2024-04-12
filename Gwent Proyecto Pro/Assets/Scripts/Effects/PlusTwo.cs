using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusTwo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Cards>().name == "Increase1")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name == "UnitSilverOrc1" || currentCard.name == "UnitSilverOrc2" || currentCard.name == "UnitSilverOrc3"))
                    {
                        currentCard.attack += 5;
                    }
                    else if (currentCard.type == "Unit")
                        currentCard.attack += 2;
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Increase2" || gameObject.GetComponent<Cards>().name == "IncreaseW5")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit")
                        currentCard.attack += 2;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
