using Gwent_Create_Card_Expression;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_Token;
using Gwent_Create_Card_ParserEffect;
using Gwent_Create_Card_ParameterValue;
using System;

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

                    throw new Exception($"Error al leer el archivo {file}: {e.Message}");
                }
            }
        }
    }

    public static bool CheckDiccionary(Dictionary<string, string> myParams, Dictionary<string, ParameterValue> declEffectParam)
    {
        if (declEffectParam.Count > myParams.Count)
            throw new Exception("Al definir el efecto de tu carta, tiene más parametros definidos que el efecto original");

        foreach(string key in myParams.Keys)
        {
            if (!declEffectParam.ContainsKey(key))
                throw new Exception($"{key} no se define en la declaración del efecto de tu carta");

            string type = declEffectParam[key].Type.ToString();

            if (myParams[key] != type)
                throw new Exception($"El tipo: {type}, de tu parametro, NO coincide con el del efecto orignal que es: {myParams[key]}");
        }

        return true;
    }
}
