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
                }
            }
        }
    }
}
