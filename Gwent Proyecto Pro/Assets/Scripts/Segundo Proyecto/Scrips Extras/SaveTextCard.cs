using Gwent_Create_Card_Expression;
using Gwent_Create_Card_ParserEffect;
using Gwent_Create_Card_Token;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_ParserCard;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Gwent_Create_Card_ParserCard;

public class SaveTextCard : MonoBehaviour
{
    public TMP_InputField inputField;

    public string fileName; // string donde guardare el nombre del texto

    public void SaveText()
    {
        string userInput = inputField.text; // Obtener el texto

        // Obtener Tokens
        List<(string, int)> listWords = Lexer.GetWordsAndRow(userInput, Lexer.specialCaracter);
        List<Tokens> tokens = Lexer.GetTokens(listWords);

        // Parsear la Lista de Tokens
        ParserCard parserCard = new ParserCard(tokens);
        Card cardText = parserCard.ParseCard();

        fileName = cardText.Name + ".txt";

        string folderPath = Path.Combine(Application.dataPath, "Textos/Texts Cards");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, fileName);

        File.WriteAllText(filePath, userInput);

        inputField.text = string.Empty;
    }
}
