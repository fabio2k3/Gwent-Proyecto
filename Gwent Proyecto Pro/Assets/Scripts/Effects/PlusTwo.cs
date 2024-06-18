using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusTwo : MonoBehaviour
{
    void Start()
    {
        if (gameObject.GetComponent<Cards>().name == "Increase1")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];  // obtener el gameobject

                if (currentCardObject != null)  // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name == "UnitSilverOrc1" || currentCard.name == "UnitSilverOrc2" || currentCard.name == "UnitSilverOrc3") && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // verificar que es una unidad Plata
                    {
                        currentCard.attack += 5; // aumentar en 5 el poder de ataque
                        currentCard.attackAux += 5;
                    }
                    else if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))  // verificar que no es de oro
                    {
                        currentCard.attack += 2; // aumentar en 2 el poder de ataque
                        currentCard.attackAux += 2;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Increase2" || gameObject.GetComponent<Cards>().name == "IncreaseW5")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];  // obtener el gameobject

                if (currentCardObject != null)  // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW"))  // verificar que no es de oro
                    {
                        currentCard.attack += 2;  // aumentar en 2 el poder de ataque
                        currentCard.attackAux += 2;
                    }
                }
            }
        }
    }
}
