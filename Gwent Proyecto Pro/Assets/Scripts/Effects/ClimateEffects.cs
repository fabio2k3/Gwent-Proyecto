using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region  Orc Climate
        if (gameObject.GetComponent<Cards>().name == "Climate1")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 3;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 3;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate2")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate3")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate4")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate5")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
        }
        #endregion

        #region Warrior Climate
        else if (gameObject.GetComponent<Cards>().name == "ClimateW1")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW2")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 3;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 3;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW3")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack -= 5;

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW4")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW5")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col];

                if (currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))
                    {
                        currentCard.attack = 0;
                    }
                }
            }
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
