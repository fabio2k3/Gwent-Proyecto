using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateEffects : MonoBehaviour
{
    void Start()
    {
        #region  Orc Climate
        if (gameObject.GetComponent<Cards>().name == "Climate1")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 3; // disminuye en 3 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 3; // disminuye en 3 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate2")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate3")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate4")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            { 
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Climate5")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
        }
        #endregion

        #region Warrior Climate
        else if (gameObject.GetComponent<Cards>().name == "ClimateW1")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col]; // obtener el gameobject

                if (currentCardObject != null)  // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW2")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 3; // disminuye en 3 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 3; // disminuye en 3 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW3")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack -= 5; // disminuye en 5 el ataque de las cartas

                        if (currentCard.attack < 0)
                            currentCard.attack = 0;
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW4")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[0, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[1, col]; // obtener el gameobject

                if (currentCardObject != null)  // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[4, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[5, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClimateW5")
        {
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[2, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
            for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
            {
                GameObject currentCardObject = DragAndDrop.gameObjectsCards[3, col]; // obtener el gameobject

                if (currentCardObject != null) // verificar si no es nulo
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();  // obtener las propiedades de Cards

                    if (currentCard.type == "Unit" && (currentCard.name != "UnitGoldOrc" || currentCard.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                    {
                        currentCard.attack = 0; // Lleva a cero el poder de ataque
                    }
                }
            }
        }
        #endregion
    }
}
