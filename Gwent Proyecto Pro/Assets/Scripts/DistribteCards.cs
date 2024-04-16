using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>();

    public List<GameObject> hand = new List<GameObject>();

    public Transform canvasTransform;
    public List<string> objectNames = new List<string>();

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject temp = deck[Random.Range(0, deck.Count)];
            hand.Add(temp);
            deck.Remove(temp);
        }

        Transform handPosition = transform.Find("CardsHand");
        

        for (int i = 0; i < hand.Count; i++)
        { 
            GameObject card = hand[i];
            Transform pos = handPosition.GetChild(i);
            
            GameObject newInstance = Instantiate(card, pos.localPosition, Quaternion.identity);
            objectNames.Add(card.name + "(Clone)");
            newInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); 
        }

        foreach (string objectName in objectNames)
        {
            GameObject obj = GameObject.Find(objectName);
            if (obj != null)
            {
                obj.transform.SetParent(canvasTransform, false);
            }
        }
    }
}
