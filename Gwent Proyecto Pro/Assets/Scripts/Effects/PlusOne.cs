using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlusOne : MonoBehaviour
{ 
    void Start()
    {
      
        if(gameObject.GetComponent<Cards>().name == "Increase3")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col]; // obtener el gameobject

                if (currentCardObject != null)  // verificar que no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (gameObject.GetComponent<Cards>().rowInvocated == 2)
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))  // verificar que no es una unidad de Oro
                        {
                            currentCard.attack += 2;  // aumentar en 2 el poder de ataque
                            currentCard.attackAux += 2;
                        }
                    }
                    else
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // verificar que no es una unidad de Oro
                        {
                            currentCard.attack += 1;  // aumentar en 1 el poder de ataque
                            currentCard.attackAux += 1;
                        }
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "IncreaseW1")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col]; // obtener el gameobject

                if (currentCardObject != null)  // verificar que no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (gameObject.GetComponent<Cards>().rowInvocated == 4)
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))  // verificar que no es una unidad de Oro
                        {
                            currentCard.attack += 3;  // aumentar en 3 el poder de ataque
                            currentCard.attackAux += 3;
                        }
                    }
                    else
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))  // verificar que no es una unidad de Oro
                        {
                            currentCard.attack += 1;  // aumentar en 1 el poder de ataque
                            currentCard.attackAux += 1;
                        }
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "IncreaseW2")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col]; // obtener el gameobject

                if (currentCardObject != null)  // verificar que no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.name == "UnitSilverW1"|| currentCard.name == "UnitSilverW2" || currentCard.name == "UnitSilverW3")
                    {
                        currentCard.attack += 2;   // aumentar en 2 el poder de ataque
                        currentCard.attackAux += 2;
                    }
                    else
                    {
                        if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // verificar que no es una unidad de Oro
                        {
                            currentCard.attack += 1;  // aumentar en 1 el poder de ataque
                            currentCard.attackAux += 1;
                        }
                    }
                }
            }
        }
    }
}
