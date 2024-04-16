using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DecoyEffect : MonoBehaviour
{
    public List<string> cardNameGame = new List<string>();
    public List<string> cardNameOriginal = new List<string>();
    public TextMeshPro nameOfUnity;
    public TextMeshPro command;
    private GameObject currentCardObject;
    private Cards currentCard;

    void Start()
    {
        for (int col = 0; col < 5; col++)
        {
            currentCardObject = DragAndDrop.gameObjectsCards[gameObject.GetComponent<Cards>().rowInvocated, col];
            if (currentCardObject != null)
            {
                currentCard = currentCardObject.GetComponent<Cards>();

                if (currentCard.type == "Unit" && cardNameGame.Contains(currentCard.name))
                {
                    nameOfUnity.gameObject.SetActive(true);
                    command.gameObject.SetActive(true);

                    int index = cardNameGame.IndexOf(currentCard.name);

                    nameOfUnity.text = " " + cardNameOriginal[index];

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        currentCard.transform.position = currentCard.originalPosition;
                        this.enabled = false;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                        continue;
                }
            }
        }
    }
}
