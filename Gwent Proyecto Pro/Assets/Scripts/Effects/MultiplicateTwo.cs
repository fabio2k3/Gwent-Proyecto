using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicateTwo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int col = 0; col < 5; col++)
        {
            GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col]; // obtener el gameObject

            if (currentCardObject != null) // verificar que no es nulo
            {
                Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // verificar que no es una unidad de ORO
                {
                    currentCard.attack *= 2; // multiplicar por 2 el aumento
                    currentCard.attackAux *= 2;
                }
            }
        }
    }
}
