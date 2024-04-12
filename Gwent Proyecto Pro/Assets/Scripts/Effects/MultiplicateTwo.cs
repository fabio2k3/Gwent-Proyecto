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
            GameObject currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];

            if (currentCardObject != null)
            {
                Cards currentCard = currentCardObject.GetComponent<Cards>();

                if (currentCard.type == "Unit")
                {
                    currentCard.attack *= 2;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
