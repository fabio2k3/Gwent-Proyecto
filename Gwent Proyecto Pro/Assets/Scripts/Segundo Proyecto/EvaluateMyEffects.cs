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
    public NewBehaviourScript warriors;
    
    // Refencia para el deck y la mano de orcs
    public NewBehaviourScript orcs;
    
    // Referencia a mi tablero
    GameObject[,] myBoard = DragAndDrop.gameObjectsCards;

    // Carpeta donde se encuentran las cartas
    private string pathTexts = "Assets/Texts/TextsEffects";
    private string pathCards = "Assets/Texts/Used";
    private string pathPrefabsOrc = "Assets/Prefabs/2nd Project/Created/Orc";
    private string pathPrefabsWarrior = "Assets/Prefabs/2nd Project/Created/Warrior";

    private void Start()
    {
        Debug.Log("Entre");
        EvaluateEffectCards(pathPrefabsOrc);
        EvaluateEffectCards(pathPrefabsWarrior);
    }



    // Metodo para Evaluar los efectos de mis Cartas en el juego (Recibe le path donde se
    //  encuentran mis cartas)
    void EvaluateEffectCards(string path)
    {
        Debug.Log("Entre2");
        // Obtener los prefabs de la carpeta
        string[] prefaFiles = Directory.GetFiles(path, "*.prefab");

        // Obtener los textos de los efectos
        string[] effectsFiles = Directory.GetFiles(pathTexts, "*.txt");

        // Obtener los textos de las cartas
        string[] textCards = Directory.GetFiles(pathCards, "*.txt");

        // Si en la carptea hay elementos"*.txt"
        if (prefaFiles.Length > 0)
        {
            // Iterar por cada prefabs de mi carpeta
            foreach (string prefab in prefaFiles)
            {
                // Cargar mi prefab como un GameObject
                GameObject myPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefab);

                Debug.Log(myPrefab.GetComponent<Cards>().invocated);

      
                if(!myPrefab.GetComponent<Cards>().invocated)
                {
                    Debug.Log("Entre");
                    if (myPrefab != null)
                    {
                        Debug.Log("No es NULO");
                        // Iterar por cada texto de las cartas
                        foreach (string textCard in textCards)
                        {
                            string textCardName = Path.GetFileNameWithoutExtension(textCard);

                            // Si el Nombre de algun texto coincide con el Nombre de mi carta
                            if (textCardName.Replace(" ", "") == myPrefab.GetComponent<Cards>().name.Replace(" ", ""))
                            {
                                // Obterner el texto de mi carta
                                string contentCard = File.ReadAllText(textCard);

                                // Realizar todo el procedimiento de Lexer y Parser
                                List<(string, int)> listWords = Lexer.GetWordsAndRow(contentCard, Lexer.specialCaracter);
                                List<Tokens> tokens = Lexer.GetTokens(listWords);

                                ParserCard parserCard = new ParserCard(tokens);
                                Card cardText = parserCard.ParseCard();

                                // Iterar por todas las declaraciones de efectos en el Action de mi Carta
                                foreach (ActivatedEffect effectsOfCards in cardText.OnActivation)
                                {
                                    // Iterar por todos los textos de mis efectos
                                    foreach (string textsEffects in effectsFiles)
                                    {
                                        string textCardEffect = Path.GetFileNameWithoutExtension(textsEffects);

                                        // Caso que coincida el texto de un efecto con el de la declaracion de mi carta
                                        if (textCardEffect.Replace(" ", "") == effectsOfCards.Effect.Name.Replace(" ", ""))
                                        {
                                            // Obtener el texto de mi Efecto
                                            string contentEffect = File.ReadAllText(textsEffects);

                                            // Realizar todo el procedimiento de Lexer y Parser
                                            List<(string, int)> wordsEffect = Lexer.GetWordsAndRow(contentEffect, Lexer.specialCaracter);
                                            List<Tokens> tokensEffect = Lexer.GetTokens(wordsEffect);

                                            ParserEffect parserEffect = new ParserEffect(tokensEffect);
                                            Effect effectText = parserEffect.ParseEffect();

                                            Evaluate(parserCard, parserEffect, cardText, effectText, effectsOfCards);
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

    void Evaluate(ParserCard parserCard, ParserEffect parserEffect, Card card, Effect effect, ActivatedEffect effectOfCard)
    {
        if(card.Faction == "Warrior")
        {
            Debug.Log("1");
            foreach(string place in effectOfCard.Selector.Source)
            {
                if(place == "hand")
                {
                    Debug.Log("2");
                    ApplyEffectToCards(warriors.hand, parserCard, parserEffect, effect, effectOfCard);
                    Debug.Log("3");
                }

                if(place == "otherHand")
                {
                    ApplyEffectToCards(orcs.hand, parserCard, parserEffect, effect, effectOfCard);
                }

                if(place == "deck")
                {
                    ApplyEffectToCards(warriors.deck, parserCard, parserEffect, effect, effectOfCard);
                }

                if(place == "otherDeck")
                {
                    ApplyEffectToCards(orcs.deck, parserCard, parserEffect, effect, effectOfCard);
                }
            }
        }
        if(card.Faction == "Orc")
        {
            foreach (string place in effectOfCard.Selector.Source)
            {
                if (place == "hand")
                {
                    ApplyEffectToCards(orcs.hand, parserCard, parserEffect, effect, effectOfCard);
                }

                if (place == "otherHand")
                {
                    ApplyEffectToCards(warriors.hand, parserCard, parserEffect, effect, effectOfCard);
                }

                if (place == "deck")
                {
                    ApplyEffectToCards(orcs.deck, parserCard, parserEffect, effect, effectOfCard);
                }

                if (place == "otherDeck")
                {
                    ApplyEffectToCards(warriors.deck, parserCard, parserEffect, effect, effectOfCard);
                }

            }
        }
    }

    private void ApplyEffectToCards(List<GameObject> cards, ParserCard parserCard, ParserEffect parserEffect, Effect effect, ActivatedEffect effectOfCard)
    {
        foreach (GameObject carta in cards)
        {
            bool predicate = parserCard.EvaluatePredicate(effectOfCard.Selector.Predicate as Expression.BinaryExpression, carta.GetComponent<Cards>().faction, carta.GetComponent<Cards>().attack);

            if (carta.GetComponent<Cards>().type == "Unit" && predicate)
            {
                Debug.Log(carta.GetComponent<Cards>().name + " " + carta.GetComponent<Cards>().attack);
                int attack = parserEffect.EvaluateAttributeModification(effect.Action, effectOfCard.Effect.Params, carta.GetComponent<Cards>().attack);
                Debug.Log(attack);
                carta.GetComponent<Cards>().attack = attack;

                if (effectOfCard.Selector.Single == "false")
                    break; // Modificar solo una carta si Single es false
            }
        }
    }
}
