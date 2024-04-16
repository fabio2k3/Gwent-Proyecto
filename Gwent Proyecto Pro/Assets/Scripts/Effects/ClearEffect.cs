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

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Cards>().name == "Clear1")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos];

                if(currentCardObject != null)
                {

                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "ClimateW1")
                    {
                        Debug.Log("Enre");
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f);
                        removeClimateCard = true;
                        positionOfRemoveClimate = pos;
                    }
                }
                
            }

        }
        else if (gameObject.GetComponent<Cards>().name == "Clear2")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos];

                if(currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "ClimateW2")
                    {
                        Debug.Log("Enre");
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f);
                        removeClimateCard = true;
                        positionOfRemoveClimate = pos;
                    }
                }
                
            }
        }
        else if (gameObject.GetComponent<Cards>().name == "Clear3")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos];

                if(currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "ClimateW3")
                    {
                        Debug.Log("Enre");
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f);
                        removeClimateCard = true;
                        positionOfRemoveClimate = pos;
                    }
                }
                
            }

        }
        else if (gameObject.GetComponent<Cards>().name == "ClearW1")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos];

                if(currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "Climate1")
                    {
                        Debug.Log("Enre");
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f);
                        removeClimateCard = true;
                        positionOfRemoveClimate = pos;
                    }
                }
                
            }


        }
        else if (gameObject.GetComponent<Cards>().name == "ClearW2")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos];

                if(currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "Climate2")
                    {
                        Debug.Log("Enre");
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f);
                        removeClimateCard = true;
                        positionOfRemoveClimate = pos;
                    }
                }
                
            }

        }
        else if (gameObject.GetComponent<Cards>().name == "ClearW3")
        {
            for (int pos = 0; pos < 3; pos++)
            {
                GameObject currentCardObject = DragAndDrop.climateCards[pos];

                if(currentCardObject != null)
                {
                    Cards currentCard = currentCardObject.GetComponent<Cards>();

                    if (currentCard.name == "Climate3")
                    {
                        Debug.Log("Enre");
                        currentCardObject.transform.position = new Vector3(currentCardObject.transform.position.x, currentCardObject.transform.position.y, 0f);
                        removeClimateCard = true;
                        positionOfRemoveClimate = pos;
                    }
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
