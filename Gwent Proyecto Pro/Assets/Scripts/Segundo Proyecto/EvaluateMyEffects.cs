using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Gwent_Create_Card_Token;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_ParserCard;
using Gwent_Create_Card_ActivatedEffect;
using Gwent_Create_Card_ParserEffect;
using Gwent_Create_Card_Expression;


public class EvaluateMyEffects : MonoBehaviour
{
    // Referencia para el deck y la mano de warriors
    NewBehaviourScript warriors = new NewBehaviourScript();
    List<GameObject> deckWarriors = new List<GameObject>();
    List<GameObject> handWarriors = new List<GameObject>();

    // Refencia para el deck y la mano de orcs
    NewBehaviourScript orcs = new NewBehaviourScript();
    List<GameObject> deckOrcs = new List<GameObject>();
    List<GameObject> handOrcsrs = new List<GameObject>();

    // Referencia a mi tablero
    GameObject[,] myBoard = DragAndDrop.gameObjectsCards;

    // Carpeta donde se encuentran las cartas
    private string pathTexts = "Assets/Textos/TextsEffects";
    private string pathCards = "Assets/Textos/Used";

    // Start is called before the first frame update
    void Start()
    {
       
    }
}
