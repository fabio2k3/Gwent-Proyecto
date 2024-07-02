using Gwent_Create_Card_Expression;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_ParserEffect;
using Gwent_Create_Card_Token;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveTextEffect : MonoBehaviour
{
    public TMP_InputField inputField;

    public string fileName; // string donde guardare el nombre del texto

    public string folderName = "Texts Effects"; // Carpeta donde guardare los textos

    public void SaveText()
    {
        string userInput = inputField.text;
        List<(string,int)> listWords = Lexer.GetWordsAndRow(userInput, Lexer.specialCaracter);
        List<Tokens> tokens = Lexer.GetTokens(listWords);

        ParserEffect parserEffect = new ParserEffect(tokens);
        Effect effect = parserEffect.ParseEffect();

        fileName = effect.Name + ".txt";

        Debug.Log(fileName);

        string folderPath = Path.Combine(Application.dataPath,folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath,fileName);

        File.WriteAllText(filePath, userInput);
    }
}
