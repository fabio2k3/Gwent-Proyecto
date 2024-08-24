using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private string pathTexts = "Assets/Textos/Texts Effects";

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
