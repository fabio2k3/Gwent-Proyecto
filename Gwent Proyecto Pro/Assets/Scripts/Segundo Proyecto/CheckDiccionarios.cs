using Gwent_Create_Card_Expression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_Token;
using Gwent_Create_Card_ParserEffect;

public class CheckDiccionarios : MonoBehaviour
{
    public static List<Effect> effects;

    private string pathTexts = "Assets/Textos/Texts Effects";
    void Start()
    {
        effects = new List<Effect>();

        string pathOfTexts = Path.Combine(Application.dataPath, pathTexts);

        if(Directory.Exists(pathTexts))
        {
            string[] txt = Directory.GetFiles(pathTexts);

            foreach(string file in txt)
            {
                try
                {
                    string content = File.ReadAllText(file);
                    List<(string, int)> listOfWords = Lexer.GetWordsAndRow(content, Lexer.specialCaracter);
                    List<Tokens> listTokens = Lexer.GetTokens(listOfWords);

                    ParserEffect parser = new ParserEffect(listTokens);
                    Effect effect = parser.ParseEffect();

                    effects.Add(effect);
                }
                catch (IOException e)
                {

                    Debug.LogError($"Error al leer el archivo {file}: {e.Message}");
                }
            }
        }
    }
}
