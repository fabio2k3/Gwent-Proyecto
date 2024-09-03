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
using UnityEditor;
using Unity.Collections.LowLevel.Unsafe;
using System.Linq;



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
    private string pathPrefabsOrc = "Assets/Prefabs/2nd Project/Created/Orc";
    private string pathPrefabsWarrior = "Assets/Prefabs/2nd Project/Created/Warrior";

    void Start()
    {

    } 

    // Metodo para Evaluar los efectos de mis Cartas en el juego (Recibe le path donde se
    //  encuentran mis cartas)
    void EvaluateEffectCards(string path)
    {
        // Obtener los prefabs de la carpeta
        string[] prefaFiles = Directory.GetFiles(path, "*.prefab");

        // Obtener los textos de los efectos
        string[] effectsFiles = Directory.GetFiles(pathTexts, "*.prefab");

        // Obtener los textos de las cartas
        string[] textCards = Directory.GetFiles(path, "*.txt");

        // Si en la carptea hay elementos
        if (prefaFiles.Length > 0)
        {
            // Iterar por cada prefabs de mi carpeta
            foreach (string prefab in prefaFiles)
            {
                // Cargar mi prefab como un GameObject
                GameObject myPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefab);

                if (myPrefab != null) 
                {
                    // Iterar por cada texto de las cartas
                    foreach(string textCard in textCards)
                    {
                        // Si el Nombre de algun texto coincide con el Nombre de mi carta
                        if(textCard == myPrefab.GetComponent<Cards>().name)
                        {
                            // Obterner el texto de mi carta
                            string contentCard = File.ReadAllText(textCard);

                            // Realizar todo el procedimiento de Lexer y Parser
                            List<(string, int)> listWords = Lexer.GetWordsAndRow(contentCard, Lexer.specialCaracter);
                            List<Tokens> tokens = Lexer.GetTokens(listWords);

                            ParserCard parserCard = new ParserCard(tokens);
                            Card cardText = parserCard.ParseCard();

                            // Iterar por todas las declaraciones de efectos en el Action de mi Carta
                            foreach(ActivatedEffect effectsOfCards in cardText.OnActivation)
                            {
                                // Iterar por todos los textos de mis efectos
                                foreach(string textsEffects in effectsFiles)
                                {
                                    // Caso que coincida el texto de un efecto con el de la declaracion de mi carta
                                    if(textsEffects == effectsOfCards.Effect.Name)
                                    {
                                        // Obtener el texto de mi Efecto
                                        string contentEffect = File.ReadAllText(textsEffects);

                                        // Realizar todo el procedimiento de Lexer y Parser
                                        List<(string, int)> wordsEffect = Lexer.GetWordsAndRow( contentEffect, Lexer.specialCaracter);
                                        List<Tokens> tokensEffect = Lexer.GetTokens(wordsEffect);

                                        ParserEffect parserEffect = new ParserEffect(tokensEffect);
                                        Effect effectText = parserEffect.ParseEffect();

                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }  
        }
    }
}
