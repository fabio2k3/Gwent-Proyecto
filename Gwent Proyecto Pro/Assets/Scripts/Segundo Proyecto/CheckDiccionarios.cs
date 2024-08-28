using Gwent_Create_Card_Expression;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_Token;
using Gwent_Create_Card_ParserEffect;
using Gwent_Create_Card_ParameterValue;
using System;

// Funcion que se encarga de verificar los parametors de los efectos de mi carta coinciden con los 
// del efecto Original

public class CheckDiccionarios : MonoBehaviour
{
    // Lista de mis efectos
    public static List<Effect> effects;
    
    // Ruta de los archivos de txt de los efectos
    private string pathTexts = "Assets/Textos/TextsEffects";

    void Start()
    {
        effects = new List<Effect>();

        // Construir la ruta completa de la carpeta que contiene los txt de mis efectos
        string pathOfTexts = Path.Combine(Application.dataPath, pathTexts);

        if(Directory.Exists(pathOfTexts))
        {
            // Obtener mis archivos txt
            string[] txt = Directory.GetFiles(pathTexts,"*.txt");

            foreach(string file in txt)
            {
                try
                {
                    // Leer el contenido de mi txt
                    string content = File.ReadAllText(file);

                    // Proceso de Tokenizacion
                    List<(string, int)> listOfWords = Lexer.GetWordsAndRow(content, Lexer.specialCaracter);
                    List<Tokens> listTokens = Lexer.GetTokens(listOfWords);

                    // Procseo de Parseo
                    ParserEffect parser = new ParserEffect(listTokens);
                    Effect effect = parser.ParseEffect();

                    // Agregar el efecto a la lista
                    effects.Add(effect);
                }
                catch (IOException e)
                {

                    throw new Exception($"Error al leer el archivo {file}: {e.Message}");
                }
            }
        }
    }

    // Metodo para verificar si los Parametros Coinciden
    public static bool CheckDiccionary(Dictionary<string, string> myParams, Dictionary<string, ParameterValue> declEffectParam)
    {
        // Verificar si hay mas parametros declardos que del Original
        if (declEffectParam.Count > myParams.Count)
            throw new Exception("Al definir el efecto de tu carta, tiene más parametros definidos que el efecto original");

        foreach(string key in myParams.Keys)
        {
            // Verificar si el parametro declarado en mi Carta es Correcto
            if (!declEffectParam.ContainsKey(key))
                throw new Exception($"{key} no se define en la declaración del efecto de tu carta");

            // Obtener el Tipo de mi efecto
            string type = declEffectParam[key].Type.ToString();

            // Verificar el tipo de mi efecto
            if (myParams[key] != type)
                throw new Exception($"El tipo: {type}, de tu parametro, NO coincide con el del efecto orignal que es: {myParams[key]}");
        }

        return true;
    }
}
