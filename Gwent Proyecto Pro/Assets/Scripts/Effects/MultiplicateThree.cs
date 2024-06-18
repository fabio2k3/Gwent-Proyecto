using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicateThree : MonoBehaviour
{
    void Start()
    {
        if (gameObject.GetComponent<Cards>().name == "IncreaseW3")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col]; // Obtener el gameobject 

                if (currentCardObject != null) // verificar que no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de la carta

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // verificar que la carta es de unidad y no es de ORO
                    {
                        currentCard.attack *= 3; // multiplicar por 3 el poder de ataque
                        currentCard.attackAux *= 3;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Increase6")
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col]; // Obtener el gameobject 

                if (currentCardObject != null)  // verificar que no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "UnitSIlverOrc1" || currentCard.name == "UnitSIlverOrc1" || currentCard.name == "UnitSIlverOrc1") // verificar es una carta Unidad Plata
                    {
                        currentCard.attack += 2; // aumentar en 2 el poder de ataque
                        currentCard.attackAux += 2;
                    }

                    else if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // verificar que la carta es de unidad y no es de ORO
                    {
                        currentCard.attack *= 3; // multiplicar por 3 el poder de ataque
                        currentCard.attackAux *= 3;
                    }
                }
            }
        }
    }
}
