using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> hand = new List<GameObject>();
    public List<GameObject> cementery = new List<GameObject>();
    public Transform canvasTransform;
    public List<string> objectNames = new List<string>();
    public Dictionary<GameObject, int> originalHandPositions = new Dictionary<GameObject, int>(); // Almacena las posiciones originales de las cartas en la mano

    public Button button; // Botón para agregar cartas desde el mazo

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = deck[Random.Range(0, deck.Count)];
            hand.Add(temp);
            deck.Remove(temp);
        }

        Transform handPosition = transform.Find("CardsHand");

        // Almacenar las posiciones originales de las cartas en la mano antes de removerlas
        for (int i = 0; i < hand.Count; i++)
        {
            originalHandPositions.Add(hand[i], i);
        }

        // Mostrar en pantalla los GameObjects y sus respectivos índices
        foreach (KeyValuePair<GameObject, int> entry in originalHandPositions)
        {
            Debug.Log("GameObject: " + entry.Key.name + ", Índice: " + entry.Value);
        }

        InstantiateCards(hand, handPosition);

        foreach (string objectName in objectNames)
        {
            GameObject obj = GameObject.Find(objectName);
            if (obj != null)
            {
                obj.transform.SetParent(canvasTransform, false);
            }
        }

        // Asignar la función al botón
        if (button != null)
        {
            button.onClick.AddListener(AddCardsFromDeckButton);
        }
    }

    void AddCardsFromDeckButton()
    {
        // Buscar elementos que ya no estén en hand pero están en el diccionario de posiciones originales
        List<GameObject> missingElements = new List<GameObject>();
        foreach (KeyValuePair<GameObject, int> entry in originalHandPositions)
        {
            if (!hand.Contains(entry.Key))
            {
                missingElements.Add(entry.Key);
            }
        }

        // Agregar un elemento aleatorio de deck a hand y al diccionario
        if (deck.Count > 0)
        {
            GameObject newCard = deck[Random.Range(0, deck.Count)];

            // Obtener la primera posición vacía en hand y agregar la carta robada
            int positionIndex = originalHandPositions[missingElements[0]];
            hand.Insert(positionIndex, newCard);
            objectNames.Add(newCard.name + "(Clone)");

            // Instanciar la carta en la posición ajustada dentro del canvas
            Transform handPosition = transform.Find("CardsHand");
            Transform targetPosition = handPosition.GetChild(positionIndex);
            GameObject newInstance = Instantiate(newCard, canvasTransform); // Padre es el canvas
            newInstance.transform.localPosition = targetPosition.localPosition; // Usamos localPosition
            newInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Eliminar la carta del mazo
            deck.Remove(newCard);

            // Eliminar la entrada del diccionario correspondiente a la posición ocupada por la nueva carta
            foreach (KeyValuePair<GameObject, int> entry in originalHandPositions.ToList())
            {
                if (entry.Value == positionIndex)
                {
                    originalHandPositions.Remove(entry.Key);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("No hay más cartas en el mazo.");
        }
    }

    void InstantiateCards(List<GameObject> cards, Transform position)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject card = cards[i];
            Transform pos = position.GetChild(i);

            GameObject newInstance = Instantiate(card, pos.localPosition, Quaternion.identity);
            objectNames.Add(card.name + "(Clone)");
            newInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
