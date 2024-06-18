using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DecoyEffect : MonoBehaviour
{
    
    void Start()
    {
        // obtener la posicion original de la carta que se sustituida
        Vector3 originalPosition = gameObject.GetComponent<Cards>().originalPosition;

        
        for (int col = 0; col < 5; col++)
        {
            // Obtener la carta en la posici�n actual
            GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

            // Verificar si hay una carta en esta posici�n y si no es la misma carta actual
            if (currentCardObject != null && currentCardObject != gameObject)
            {
                // Establecer las coordenadas originales al GameObject actual
                currentCardObject.transform.localPosition = originalPosition;
                DragAndDrop.gameObjectsCards[currentCardObject.GetComponent<Cards>().rowInvocated, currentCardObject.GetComponent<Cards>().colInvocated] = null;
            }
        }
    }
}
