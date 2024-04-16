using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DecoyEffect : MonoBehaviour
{
    public static bool confirmed;

    public static bool returnCard;

    void Start()
    {
        for (int col = 0; col < 5; col++)
        {
            GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

            if (currentCardObject != null)
            {
                Cards currentCard = currentCardObject.GetComponent<Cards>();

                if (currentCard.type == "Unit")
                {
                    if(confirmed)
                    {
                        currentCardObject.transform.position = currentCard.originalPosition;

                        returnCard = true;
                    }
                }
            }
        }
    }
}
