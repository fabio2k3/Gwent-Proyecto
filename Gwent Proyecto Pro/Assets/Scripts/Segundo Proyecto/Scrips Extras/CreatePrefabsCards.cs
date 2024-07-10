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
    string newTextFolder = "Assets/Textos/Used";

    // Rutas Carpetas Prefabs
    string prefabsFolderOrc = "Assets/Prefabs/2nd Project/For Use/Orc";
    string prefabsFolderWarrior = "Assets/Prefabs/2nd Project/For Use/Warrior";

    // Rutas para Prefabs Creados
    string createdOrcCard = "Assets/Prefabs/2nd Project/Created/Orc";
    string createdWarriorCard = "Assets/Prefabs/2nd Project/Created/Warrior";

    public void CreatePrefabs()
    {
        // Array de todos los txt
        string[] textCards = Directory.GetFiles(textFolder, "*.txt");
   
        // Por cada Carta Crear un Prefa (una Carta)
        foreach (string textCard in textCards)
        {
            string content = File.ReadAllText(textCard);
            if (content.StartsWith("card"))
            {
                // Obtener el string del txt
                Debug.Log($"Contenido del archivo {textCard}: {content}");

                // Proceso de Tokenizacion & Lexer
                List<(string, int)> listWords = Lexer.GetWordsAndRow(content, Lexer.specialCaracter);
                List<Tokens> tokens = Lexer.GetTokens(listWords);

                // Parsear la lista de Tokens (Cartas)
                ParserCard parserCard = new ParserCard(tokens);
                Card card = parserCard.ParseCard();

                Debug.Log($"Tipo: {card.Type}");
                Debug.Log($"Nombre: {card.Name}");
                Debug.Log($"Facción: {card.Faction}");
                Debug.Log($"Poder: {card.Power}");

                // Caso que sea orco
                if (card.Faction == "Orc")
                {
                    string[] prefabFiles = Directory.GetFiles(prefabsFolderOrc, "*.prefab");
                    if (prefabFiles.Length > 0)
                    {
                        // Tomo el Primer Prefab & lo cargo como un GameObject
                        string firstPrefabPath = prefabFiles[0];
                        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(firstPrefabPath);

                        CompletingPropieties(prefab, card);

                        // Mover mi Prefab de Carpeta
                        string newPrefabPath = Path.Combine(createdOrcCard, Path.GetFileName(firstPrefabPath));
                        AssetDatabase.MoveAsset(firstPrefabPath, newPrefabPath);
                    }
                }
                else if (card.Faction == "Warrior") // Caso que sea Warrior
                {
                    string[] prefabFiles = Directory.GetFiles(prefabsFolderWarrior, "*.prefab");
                    Debug.Log(prefabFiles.Length);
                    if (prefabFiles.Length > 0)
                    {
                        // Tomo el Primer Prefab & lo cargo como un GameObject
                        string firstPrefabPath = prefabFiles[0];
                        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(firstPrefabPath);

                        CompletingPropieties(prefab, card);

                        // Mover mi Prefab de Carpeta
                        string newPrefabPath = Path.Combine(createdWarriorCard, Path.GetFileName(firstPrefabPath));
                        AssetDatabase.MoveAsset(firstPrefabPath, newPrefabPath);
                    }
                }
            }

            // Mover los txt a la carpeta destino
            string newFilePath = Path.Combine(newTextFolder, Path.GetFileName(textCard));
            File.Move(textCard, newFilePath);
        }
    }

    // Metodo para Completar las Propiedades de mi Carta
    private void CompletingPropieties(GameObject myPrefab, Card card)
    {
        Cards cardComponent = myPrefab.GetComponent<Cards>();
        if (cardComponent != null)
        {
            // Optener su Nombre & Tipo
            cardComponent.name = card.Name;
            cardComponent.type = card.Type;
            Debug.Log($"Propiedades actualizadas: Name = {cardComponent.name}, Type = {cardComponent.type}");

            #region Casos Especiales: Clima & Aumento
            if (card.Type == "Clima")
            {
                Debug.Log("Las Cartas tipo Clima tienen sus características especiales por default");
                cardComponent.attack = 0;
                myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "Climate1", "Climate2", "Climate13" });
            }
            else if (card.Type == "Aumento" && card.Faction == "Warrior")
            {
                Debug.Log("Las Cartas tipo Aumento tienen sus características especiales por default");
                cardComponent.attack = 0;
                myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "CW1", "CW2", "CW3" });
            }
            else if (card.Type == "Aumento" && card.Faction == "Orc")
            {
                Debug.Log("Las Cartas tipo Aumento tienen sus características especiales por default");
                cardComponent.attack = 0;
                myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "CO1", "CO2", "CO3" });
            }
            #endregion

            else if (card.Type == "Oro" || card.Type == "Plata")
            {
                cardComponent.attack = card.Power;
                Debug.Log($"Ataque actualizado: Attack = {cardComponent.attack}");

                if (card.Faction == "Warrior")
                {
                    if (card.Range.Contains("Melee"))
                    {
                        myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "MW1", "MW2", "MW3", "MW4", "MW5" });
                    }
                    if (card.Range.Contains("Ranged"))
                    {
                        myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "RW1", "RW2", "RW3", "RW4", "RW5" });
                    }
                    if (card.Range.Contains("Siege"))
                    {
                        myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "SW1", "SW2", "SW3", "SW4", "SW5" });
                    }
                }
                else if (card.Faction == "Orc")
                {
                    if (card.Range.Contains("Melee"))
                    {
                        myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "MO1", "MO2", "MO3", "MO4", "MO5" });
                    }
                    if (card.Range.Contains("Ranged"))
                    {
                        myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "RO1", "RO2", "RO3", "RO4", "RO5" });
                    }
                    if (card.Range.Contains("Siege"))
                    {
                        myPrefab.GetComponent<DragAndDrop>().validPositions.AddRange(new List<string> { "SO1", "SO2", "SO3", "SO4", "SO5" });
                    }
                }
            }

            Debug.Log("Propiedades completadas para el prefab: " + myPrefab.name);
        }
    }
}