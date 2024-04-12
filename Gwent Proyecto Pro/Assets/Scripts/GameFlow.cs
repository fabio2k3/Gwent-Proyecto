using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public Cards[,] cardsOnBoard = new Cards[6, 6];
    private bool[,] effectActivatedOnTheCard = new bool[6, 6];

    public static bool avoidCampOrc;
    public static bool avoidCampWarrior;

    private static int prueba = 0;


    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}

