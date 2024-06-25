using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_Token;

public class LoadTextEffect : MonoBehaviour
{
    public void loadText()
    {
        if(SaveTextEffect.savedTexts.Count == 0)
        {
            UnityEngine.Debug.Log("No hay textos guardados");
            return;
        }

        UnityEngine.Debug.Log("Hola");

        foreach(var textKeyValue in SaveTextEffect.savedTexts)
        {
           
            string text = textKeyValue.Value;

            List<string> words = Lexer.GetWords(text);
            List<Tokens> tokens = Lexer.GetTokens(words);

            foreach (Tokens token in tokens)
                UnityEngine.Debug.Log($"Token:{token.Value}, Tipo: {token.Type}");
        }
    }
}
