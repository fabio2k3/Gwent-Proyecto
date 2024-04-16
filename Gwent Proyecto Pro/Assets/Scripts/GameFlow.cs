using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class GameFlow : MonoBehaviour
{
    public static bool avoidCampOrc;
    public static bool avoidCampWarrior;

    private static int whoStart;
    private int orcWinsCount = 0;
    private int warriorWinsCount = 0;

    public TextMeshProUGUI scoreWarrior;
    public TextMeshProUGUI scoreOrc;
    public GameObject roundOfWarriors;
    public GameObject roundOfOrcs;
    public GameObject WarriorsWin;
    public GameObject OrcWin;

    public List<string> ClimateCards = new List<string>();

    private bool orcGave;
    private bool warriorsGave;
    private bool weHaveWinner;

    public NewBehaviourScript cardHandOrc;
    public NewBehaviourScript cardHandWarrior; 

    void Start()
    {
        scoreWarrior = GameObject.Find("ScoreWarriors").GetComponent<TextMeshProUGUI>(); 
        scoreOrc = GameObject.Find("ScoreOrcs").GetComponent<TextMeshProUGUI>();

        whoStart = UnityEngine.Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Estado actual " + whoStart.ToString());

        #region Clear Effect !!!!
        if(ClearEffect.removeClimateCard)
        {
            if (DragAndDrop.climateCards[ClearEffect.positionOfRemoveClimate] != null)
            {
                string cardName = DragAndDrop.climateCards[ClearEffect.positionOfRemoveClimate].name;
                GameObject cloneObject = DragAndDrop.climateCards[ClearEffect.positionOfRemoveClimate];

                string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                if (cardHandOrc.objectNames.Contains(cardName))
                {
                    cardHandOrc.objectNames.Remove(cardName);
                    // Eliminar el objeto de hand si coincide con el objeto original
                    cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                }
                else if (cardHandWarrior.objectNames.Contains(cardName))
                {
                    cardHandWarrior.objectNames.Remove(cardName);
                    // Eliminar el objeto de hand si coincide con el objeto original
                    cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);
                }

                DragAndDrop.climateCards[cloneObject.GetComponent<Cards>().climateInvocated] = null;
            }
        }
        #endregion

        #region Winner of the GAME

        if (orcWinsCount >= 2)
        {
            OrcWin.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (warriorWinsCount >= 2)
        {
            WarriorsWin.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        #endregion

        #region Clear the Board
        if (weHaveWinner && Input.GetKeyDown(KeyCode.Return))
        {
            for (int row = 0; row < DragAndDrop.gameObjectsCards.GetLength(0); row++)
            {
                for (int col = 0; col < DragAndDrop.gameObjectsCards.GetLength(1); col++)
                {
                    if (DragAndDrop.gameObjectsCards[row, col] != null)
                    {
                        string cardName = DragAndDrop.gameObjectsCards[row, col].name;
                        GameObject clonedObject = DragAndDrop.gameObjectsCards[row, col];

                        string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                        if (cardHandOrc.objectNames.Contains(cardName))
                        {
                            cardHandOrc.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }
                        else if (cardHandWarrior.objectNames.Contains(cardName))
                        {
                            cardHandWarrior.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }

                        DragAndDrop.gameObjectsCards[row, col].transform.position = new Vector3(
                            DragAndDrop.gameObjectsCards[row, col].transform.position.x,
                            DragAndDrop.gameObjectsCards[row, col].transform.position.y,
                            0f
                        );

                        DragAndDrop.gameObjectsCards[row, col] = null;
                    }                    
                }

                for(int pos = 0; pos < 3; pos++)
                {
                    if (DragAndDrop.climateCards[pos] != null)
                    {
                        string cardName = DragAndDrop.climateCards[pos].name;
                        GameObject cloneObject = DragAndDrop.climateCards[pos];

                        string originalObjectName = cardName.EndsWith("(Clone)") ? cardName.Replace("(Clone)", "") : cardName;

                        if (cardHandOrc.objectNames.Contains(cardName))
                        {
                            cardHandOrc.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandOrc.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }
                        else if (cardHandWarrior.objectNames.Contains(cardName))
                        {
                            cardHandWarrior.objectNames.Remove(cardName);
                            // Eliminar el objeto de hand si coincide con el objeto original
                            cardHandWarrior.hand.RemoveAll(obj => obj.name == originalObjectName);
                        }
                    }
                }
            }

            weHaveWinner = false;
            roundOfOrcs.SetActive(false);
            roundOfWarriors.SetActive(false);
        }
        #endregion

        #region Check Winner of the Round
        if (warriorsGave && orcGave)
        {
            if (CalculateScoreOrcs() > CalculateScoreWarriors())
            {
                Debug.Log("Gana Orcos");
                roundOfOrcs.SetActive(true);
                orcWinsCount++;
                weHaveWinner = true;
                warriorsGave = false;
                orcGave = false;
                Debug.Log(orcWinsCount);
            }
                
            else if(CalculateScoreOrcs() < CalculateScoreWarriors())
            {
                Debug.Log("Gana W");
                roundOfWarriors.SetActive(true);
                warriorWinsCount++;
                weHaveWinner = true;
                warriorsGave = false;
                orcGave = false;
                Debug.Log(warriorWinsCount);
            }
                
        }
        #endregion

        else
        {
            #region Pass Turn
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (orcGave)
                    warriorsGave = true;
                else if (warriorsGave)
                    orcGave = true;

                whoStart = (whoStart == 0) ? 1 : 0;
            }

            if (whoStart == 0)
            {
                avoidCampOrc = true;
                avoidCampWarrior = false;
            }
            else if (whoStart == 1)
            {
                avoidCampOrc = false;
                avoidCampWarrior = true;
            }
            #endregion

            scoreWarrior.text = "Score: " + CalculateScoreWarriors();
            scoreOrc.text = "Score: " + CalculateScoreOrcs();

            #region Give Up Turn
            if (Input.GetKeyDown(KeyCode.V) && !avoidCampWarrior)
            {
                warriorsGave = true;
                Debug.Log("W cedio");
                whoStart = 1;
            }
            if (Input.GetKeyDown(KeyCode.V) && !avoidCampOrc)
            {
                orcGave = true;
                Debug.Log("O cedio");
                whoStart = 0;
            }
            #endregion
        }
    }

    private int CalculateScoreOrcs()
    {
        int scoreOrc = 0;
        for (int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 5; col++)
            {
                GameObject card = DragAndDrop.gameObjectsCards[row,col];

                if (card != null)
                    scoreOrc += card.GetComponent<Cards>().attack;
            }
        }

        return scoreOrc;
    }

    private int CalculateScoreWarriors()
    {
        int scoreWarrior = 0;
        for (int row = 3; row < 6; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                GameObject card = DragAndDrop.gameObjectsCards[row, col];

                if (card != null)
                    scoreWarrior += card.GetComponent<Cards>().attack;
            }
        }

        return scoreWarrior;
    }
}

