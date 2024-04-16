using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public static List<GameObject> deck = new List<GameObject>();
    public static List<GameObject> hand = new List<GameObject>();
    public Transform canvasTransform;
    public List<string> objectNames = new List<string>();

    // M�todo para dibujar cartas en la mano
    public void Draw()
    {
        // Verificar si la lista de mazo (deck) tiene al menos un elemento
        if (deck.Count > 0)
        {
            // Agregar cartas a la mano
            for (int i = 0; i < 10; i++)
            {
                // Verificar si el mazo a�n tiene cartas antes de intentar agregar una
                if (deck.Count > 0)
                {
                    GameObject temp = deck[Random.Range(0, deck.Count)];
                    hand.Add(temp);
                    deck.Remove(temp);
                }
                else
                {
                    Debug.LogWarning("No quedan cartas en el mazo para agregar a la mano.");
                    break; // Salir del bucle si no quedan cartas en el mazo
                }
            }

            // Encontrar la posici�n de la mano en la jerarqu�a
            Transform handPosition = transform.Find("CardsHand");

            // Dibujar las cartas en la posici�n de la mano
            for (int i = 0; i < hand.Count; i++)
            {
                // Verificar si el �ndice est� dentro del rango de hijos de handPosition
                if (handPosition.childCount > i)
                {
                    GameObject card = hand[i];
                    Transform pos = handPosition.GetChild(i);

                    GameObject newInstance = Instantiate(card, pos.localPosition, Quaternion.identity);
                    objectNames.Add(card.name + "(Clone)");
                    newInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    newInstance.transform.SetParent(canvasTransform, false);
                }
                else
                {
                    Debug.LogWarning("El �ndice est� fuera del rango de hijos de handPosition.");
                    break; // Salir del bucle si el �ndice est� fuera del rango
                }
            }
        }
        else
        {
            Debug.LogWarning("La lista de mazo (deck) est� vac�a. No se pueden agregar cartas a la mano.");
        }
    }
}
