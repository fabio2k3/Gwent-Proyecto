using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public Cards[,] cardsOnBoard = new Cards[6, 6];
    private bool[,] effectActivatedOnTheCard = new bool[6, 6];

    public static bool avoidCampOrc;
    public static bool avoidCampWarrior;

    private static int prueba = 0;

    public TextMeshProUGUI scoreWarrior; // Asegúrate de asignar estos en el inspector
    public TextMeshProUGUI scoreOrc;     // Asegúrate de asignar estos en el inspector

    // Start is called before the first frame update
    void Start()
    {
        // Obtener las referencias a los componentes TextMeshProUGUI
        scoreWarrior = GameObject.Find("ScoreWarriors").GetComponent<TextMeshProUGUI>(); // Reemplaza "ScoreWarrior" por el nombre del objeto en Unity
        scoreOrc = GameObject.Find("ScoreOrcs").GetComponent<TextMeshProUGUI>();         // Reemplaza "ScoreOrc" por el nombre del objeto en Unity
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (prueba == 0)
                prueba += 1;
            else if(prueba == 1)
                prueba -= 1;
        }

        if (prueba  == 0)
        {
            avoidCampOrc = true;
            avoidCampWarrior = false;

            //if (DragAndDrop.moveACard)
            //    avoidCampWarrior = true;
        }
        else if(prueba == 1)
        {
            avoidCampOrc = false;
            avoidCampWarrior = true;

            //if (DragAndDrop.moveACard)
            //    avoidCampOrc = true;
        }

        scoreWarrior.text = "Score: " + CalculateScoreWarriors();
        scoreOrc.text = "Score: " + CalculateScoreOrcs();
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

