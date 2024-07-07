using Gwent_Create_Card_Expression;
using Gwent_Create_Card_ParserCard;
using Gwent_Create_Card_Token;
using Gwent_Create_Card_Lexer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CreatePrefabsCards : MonoBehaviour
{
    // Ruta Carpeta Textos Cartas
    string textFolder = "Assets/Textos/Texts Cards";
    // Ruta Carpeta Destino Textos Cartas
    string newTextFolder = "Assets/Textos/Texts Cards/Used";
    // Rutas Carpetas Prefabs
    string prefabsFolderOrc = "Assets/Prefabs/2nd Project/For Use/Orc";
    string prefabsFolderWarrior = "Assets/Prefabs/2nd Project/For Use/Warrior";


    // Refencias a los masos de las cartas
    NewBehaviourScript orcsDeck;
    NewBehaviourScript warriorsDeck;

    public void CreatePrefabs()
    {
        string[] textCards = Directory.GetFiles(textFolder, "*.txt");
        foreach (string textCard in textCards)
        {
            // Obtener e string del txt
            string content = File.ReadAllText(textCard);

            // Proceso de Tokenizacion & Lexer
            List<(string, int)> listWords = Lexer.GetWordsAndRow(content, Lexer.specialCaracter);
            List<Tokens> tokens = Lexer.GetTokens(listWords);

            // Parsear la lista de Tokens (Cartas)
            ParserCard parserCard = new ParserCard(tokens);
            Card card = parserCard.ParseCard();

            if(card.Type == "Orc")
            {
                string[] prefabFiles = Directory.GetFiles(prefabsFolderOrc, "*.prefab");
                if (prefabFiles.Length > 0)
                {
                    // Tomo el Primer Prefabs & lo cargo como un GameObject
                    string firstPrefabPath = prefabFiles[0];
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(firstPrefabPath);

                    CompletingPropieties(prefab, card);
                }
            }
            else if(card.Type == "Warrior")
            {
                string[] prefabFiles = Directory.GetFiles(prefabsFolderWarrior, "*.prefab");
                if (prefabFiles.Length > 0)
                {
                    // Tomo el Primer Prefabs & lo cargo como un GameObject
                    string firstPrefabPath = prefabFiles[0];
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(firstPrefabPath);

                    CompletingPropieties(prefab, card);
                }
            }
        }
    }

    private void CompletingPropieties(GameObject myPrefab, Card card)
    {
        myPrefab.GetComponent<Cards>().name = card.Name;
        myPrefab.GetComponent<Cards>().type = card.Type;
      
        if(card.Type == "Clima")
        {
            Debug.Log("Las Cartas tipo Clima tienen sus caracteristicas especiales por default");
            myPrefab.GetComponent<Cards>().attack = 0;
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("Climate1");
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("Climate2");
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("Climate13");
        }
        else if(card.Type == "Aumento" && card.Faction == "Warrior")
        {
            Debug.Log("Las Cartas tipo Aumento tienen sus caracteristicas especiales por default");
            myPrefab.GetComponent<Cards>().attack = 0;
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("CW1");
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("CW2");
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("CW3");
        }
        else if (card.Type == "Aumento" && card.Faction == "Orc")
        {
            Debug.Log("Las Cartas tipo Aumento tienen sus caracteristicas especiales por default");
            myPrefab.GetComponent<Cards>().attack = 0;
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("CO1");
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("CO2");
            myPrefab.GetComponent<DragAndDrop>().validPositions.Add("CO3");
        }
        else if(card.Type == "Oro" || card.Type == "Plata" )
        {
            myPrefab.GetComponent<Cards>().attack = card.Power;

            #region Caso Warrior
            if (card.Faction == "Warrior")
            {
                if(card.Range.Contains("Melee"))
                {
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MW1");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MW2");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MW3");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MW4");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MW5");
                }
                if(card.Range.Contains("Ranged"))
                {
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RW1");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RW2");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RW3");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RW4");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RW5");
                }
                if(card.Range.Contains("Siege"))
                {
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SW1");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SW2");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SW3");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SW4");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SW5");
                }
            }
            #endregion

            #region Caso Orc
            else if (card.Faction == "Orc")
            {
                if (card.Range.Contains("Melee"))
                {
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MO1");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MO2");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MO3");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MO4");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("MO5");
                }
                if (card.Range.Contains("Ranged"))
                {
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RO1");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RO2");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RO3");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RO4");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("RO5");
                }
                if (card.Range.Contains("Siege"))
                {
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SO1");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SO2");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SO3");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SO4");
                    myPrefab.GetComponent<DragAndDrop>().validPositions.Add("SO5");
                }
            }
            #endregion
        }
    }
}
