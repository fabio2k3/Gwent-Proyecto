using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class ClearEffect : MonoBehaviour
{
    public static bool removeClimateCard;
    public static int positionOfRemoveClimate;

    void Start()
    {
        if(gameObject.GetComponent<Cards>().name == "Clear1")
        {
            // iterar por el array
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos]; // obtener el gameobject del array

                if(currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.name == "ClimateW1") // si el nombre coincide
                    {
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f); // removerlo del array
                        removeClimateCard = true; // verificar que se removio
                        positionOfRemoveClimate = pos; // obtener la posicion donde se encuentra la carta

                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[2, col]; // obtener el gameobject

                            if (currentCardObjectAux != null)  // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>();  // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[3, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>(); // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                    }
                }
                
            }

        }
        else if (gameObject.GetComponent<Cards>().name == "Clear2")
        {
            // iterar por el array
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos]; // obtener el gameobject del array

                if (currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.name == "ClimateW2") // si el nombre coincide
                    {
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f); // removerlo del array
                        removeClimateCard = true; // verificar que se removio
                        positionOfRemoveClimate = pos; // obtener la posicion donde se encuentra la carta

                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[1, col]; // obtener el gameobject

                            if (currentCardObject != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>();  // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[4, col]; // obtener el gameobject

                            if (currentCardObject != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>();  // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                    }
                }

            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Clear3")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos]; // obtener el gameobject del array

                if (currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.name == "ClimateW3") // si el nombre coincide
                    {
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f); // removerlo del array
                        removeClimateCard = true; // verificar que se removio
                        positionOfRemoveClimate = pos; // obtener la posicion donde se encuentra la carta

                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[0, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>();  // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[5, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>(); // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                    }
                }

            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClearW1")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos]; // obtener el gameobject del array

                if (currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.name == "Climate1") // si el nombre coincide
                    {
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f); // removerlo del array
                        removeClimateCard = true; // verificar que se removio
                        positionOfRemoveClimate = pos; // obtener la posicion donde se encuentra la carta

                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject cardObject = DragAndDrop.gameObjectsCards[3, col]; // obtener el gameobject

                            if (cardObject != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = cardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject cardObject = DragAndDrop.gameObjectsCards[2, col]; // obtener el gameobject

                            if (cardObject != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = cardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux; 
                                }
                            }
                        }
                    }
                }

            }
        }
        else if (gameObject.GetComponent<Cards>().name == "ClearW2")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos]; // obtener el gameobject del array

                if (currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.name == "Climate2") // si el nombre coincide
                    {
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f); // removerlo del array
                        removeClimateCard = true; // verificar que se removio
                        positionOfRemoveClimate = pos; // obtener la posicion donde se encuentra la carta

                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[4, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>(); // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[1, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>(); // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                    }
                }

            }

        }
        else if (gameObject.GetComponent<Cards>().name == "ClearW3")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos]; // obtener el gameobject del array

                if (currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>(); // obtener las propiedades de Cards

                    if (currentCard.name == "Climate3") // si el nombre coincide
                    {
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f); // removerlo del array
                        removeClimateCard = true; // verificar que se removio
                        positionOfRemoveClimate = pos; // obtener la posicion donde se encuentra la carta

                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[5, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>();  // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                        for (int col = 0; col < 5; col++) // iterar por todas las cartas de la fila
                        {
                            GameObject currentCardObjectAux = DragAndDrop.gameObjectsCards[0, col]; // obtener el gameobject

                            if (currentCardObjectAux != null) // verificar si no es nulo
                            {
                                Cards currentCardAux = currentCardObjectAux.GetComponent<Cards>();  // obtener las propiedades de Cards

                                if (currentCardAux.type == "Unit" && (currentCardAux.name != "UnitGoldOrc" || currentCardAux.name != "UnitGoldW")) // Verificar que es una carta de unidad y no es de ORO
                                {
                                    currentCardAux.attack = currentCardAux.attackAux;
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
