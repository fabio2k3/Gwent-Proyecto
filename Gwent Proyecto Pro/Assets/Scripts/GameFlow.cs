using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public Cards[,] cardsOnBoard = new Cards[6, 6];
    private bool[,] effectActivatedOnTheCard = new bool[6, 6];

    public static bool avoidCampOrc;
    public static bool avoidCampWarrior;

    private static int prueba = 0;

    public TextMeshProUGUI scoreWarrior; // Asegúrate de asignar estos en el inspector
    public TextMeshProUGUI scoreOrc;     // Asegúrate de asignar estos en el inspector
    public GameObject roundOfWarriors;
    public GameObject roundOfOrcs;

    public List<string> ClimateCards = new List<string>();

    private bool orcGave;
    private bool warriorsGave;

    
    void Start()
    {
        // Obtener las referencias a los componentes TextMeshProUGUI
        scoreWarrior = GameObject.Find("ScoreWarriors").GetComponent<TextMeshProUGUI>(); 
        scoreOrc = GameObject.Find("ScoreOrcs").GetComponent<TextMeshProUGUI>();         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roundOfWarriors.SetActive(false);
            roundOfOrcs.SetActive(false);
        }

        if (warriorsGave && orcGave)
        {
            if (CalculateScoreOrcs() > CalculateScoreWarriors())
            {
                Debug.Log("Gana Orcos");
                roundOfOrcs.SetActive(true);
            }
                
            else if(CalculateScoreOrcs() < CalculateScoreWarriors())
            {
                Debug.Log("Gana W");
                roundOfWarriors.SetActive(true);
            }
                
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (prueba == 0)
                    prueba += 1;
                else if (prueba == 1)
                    prueba -= 1;
            }

            if (prueba == 0)
            {
                avoidCampOrc = true;
                avoidCampWarrior = false;

                //if (DragAndDrop.moveACard)
                //    avoidCampWarrior = true;
            }
            else if (prueba == 1)
            {

                avoidCampOrc = false;
                avoidCampWarrior = true;

                //if (DragAndDrop.moveACard)
                //    avoidCampOrc = true;
            }

            scoreWarrior.text = "Score: " + CalculateScoreWarriors();
            scoreOrc.text = "Score: " + CalculateScoreOrcs();

            if (Input.GetKeyDown(KeyCode.V) && !avoidCampWarrior)
            {
                warriorsGave = true;
                Debug.Log("W cedio");
                prueba += 1;
            }
            if (Input.GetKeyDown(KeyCode.V) && !avoidCampOrc)
            {
                orcGave = true;
                Debug.Log("O cedio");
                prueba -= 1;
            }
        }
        Debug.Log("Estado actual" + prueba.ToString());
        
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

