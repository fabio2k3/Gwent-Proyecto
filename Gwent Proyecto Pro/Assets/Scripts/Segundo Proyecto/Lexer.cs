using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent_Create_Card_Token;

namespace Gwent_Create_Card_Lexer
{
    public class Lexer
    {
        static string[] symbols = { "+", "-", "*", "/", "{", "}", "(", ")", "=", ">", "<", "&", "|", "\"", ";", ",", ".", ":", "^", "@" };


        // Convertir de Multi a Una Sola Linea
        public static string MultiToUniLine(string input)
        {
            string copyInput = input;

            return string.Join(" ", copyInput.Split(new[] { '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries));
        }


        // Añadir espacios en blanco en los caracteres
        public static string AddSpacesAroundChars(string input, string[] specialCaracter)
        {
            foreach (string str in specialCaracter)
            {
                input = input.Replace(str, $" {str} ");
            }

            input = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", " ");

            return input;
        }


        // Obetner Lista de Palabras
        public static List<string> GetWords(string input)
        {
            string[] tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return new List<string>(tokens);
        }


        // Verificar Si esta balanceada con respecto a {}
        public static bool IsBalancedKey(string input)
        {
            // Si la entrada no contienes Llaves esta Balanceada
            if (!input.Contains('{') && !input.Contains('}'))
                return true;

            int lastOpen = input.LastIndexOf('{');

            if (lastOpen == -1) return false;

            int firstClosed = input.Substring(lastOpen).IndexOf("}");

            if (firstClosed == -1) return false;

            input = input.Remove(firstClosed + lastOpen, 1);
            input = input.Remove(lastOpen, 1);

            return IsBalancedKey(input);
        }


        // Verificar Si esta balanceada con respecto a ()
        public static bool IsBalancedParentesis(string input)
        {
            // Si la entrada no contienes Llaves esta Balanceada
            if (!input.Contains('(') && !input.Contains(')'))
                return true;

            int lastOpen = input.LastIndexOf('(');

            if (lastOpen == -1) return false;

            int firstClosed = input.Substring(lastOpen).IndexOf(")");

            if (firstClosed == -1) return false;

            input = input.Remove(firstClosed + lastOpen, 1);
            input = input.Remove(lastOpen, 1);

            return IsBalancedKey(input);
        }

        // Obtener Lista de Tokens
        public static List<Tokens> GetTokens(List<string> words)
        {
            List<Tokens> tokens = new List<Tokens>();

            for (int i = 0; i < words.Count; i++)
            {
                if (words[i] == "=")
                {
                    if (words[i + 1] == "=")
                    {
                        tokens.Add(new Tokens("==", Tokens.TokenType.Igual));
                        i += 1;
                    }
                    else if (words[i + 1] == ">")
                    {
                        tokens.Add(new Tokens("=>", Tokens.TokenType.Arrow));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens(words[i], ClasificarToken(words[i])));
                }
                else if (words[i] == "+")
                {
                    if (words[i + 1] == "+")
                    {
                        tokens.Add(new Tokens("++", Tokens.TokenType.PlusPlus));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens(words[i], ClasificarToken(words[i])));
                }
                else if (words[i] == "<" || words[i] == ">")
                {
                    if (words[i + 1] == "=")
                    {
                        if (words[i] == "<")
                        {
                            tokens.Add(new Tokens("<=", Tokens.TokenType.MenorIgualQ));
                            i += 1;
                        }
                        else if (words[i] == ">")
                        {
                            tokens.Add(new Tokens(">=", Tokens.TokenType.MayorIgualQ));
                            i += 1;
                        }
                        else
                            tokens.Add(new Tokens(words[i], ClasificarToken(words[i])));
                    }
                }
                else if (words[i] == "@")
                {
                    if (words[i + 1] == "@")
                    {
                        tokens.Add(new Tokens(words[i], Tokens.TokenType.DobleArroba));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens(words[i], ClasificarToken(words[i])));
                }
                else if (words[i] == "+" || words[i] == "-")
                {
                    if (words[i + 1] == "=")
                    {
                        if (words[i] == "+")
                        {
                            tokens.Add(new Tokens("+=", Tokens.TokenType.PlusMood));
                            i += 1;
                        }
                        else if (words[i] == "-")
                        {
                            tokens.Add(new Tokens("-=", Tokens.TokenType.RestMood));
                            i += 1;
                        }
                        else
                            tokens.Add(new Tokens(words[i], ClasificarToken(words[i])));
                    }
                }
                else
                    tokens.Add(new Tokens(words[i], ClasificarToken(words[i])));
            }

            return tokens;
        }

        static Tokens.TokenType ClasificarToken(string token)
        {
            try
            {
                double.Parse(token);
                return Tokens.TokenType.Number;
            }
            catch (Exception)
            {
                switch (token)
                {
                    case "+":
                        return Tokens.TokenType.Sum;
                    case "-":
                        return Tokens.TokenType.Resta;
                    case "*":
                        return Tokens.TokenType.Multi;
                    case "/":
                        return Tokens.TokenType.Div;
                    case "^":
                        return Tokens.TokenType.Pow;
                    case "&&":
                        return Tokens.TokenType.And;
                    case "||":
                        return Tokens.TokenType.Or;
                    case "<":
                        return Tokens.TokenType.MenoQ;
                    case ">":
                        return Tokens.TokenType.MayorQ;
                    case "=":
                        return Tokens.TokenType.Asignacion;
                    case "@":
                        return Tokens.TokenType.Arroba;
                    case "{":
                        return Tokens.TokenType.OpenLlave;
                    case "}":
                        return Tokens.TokenType.CloseLlave;
                    case "(":
                        return Tokens.TokenType.OpenParentesis;
                    case ")":
                        return Tokens.TokenType.ClosePArentesis;
                    case ";":
                        return Tokens.TokenType.Semicolon;
                    case ",":
                        return Tokens.TokenType.Coma;
                    case ":":
                        return Tokens.TokenType.DoblePoint;
                    case ".":
                        return Tokens.TokenType.Point;
                    case "card":
                        return Tokens.TokenType.Card;
                    case "effect":
                        return Tokens.TokenType.Effect;
                    case "Name":
                        return Tokens.TokenType.Name;
                    case "Params":
                        return Tokens.TokenType.Params;
                    case "Action":
                        return Tokens.TokenType.Action;
                    case "TriggerPlayer":
                        return Tokens.TokenType.TriggerPlayer;
                    case "Board":
                        return Tokens.TokenType.Board;
                    case "HandOfPlayer":
                        return Tokens.TokenType.HandOfPlayer;
                    case "FieldOfPlayer":
                        return Tokens.TokenType.FieldOfPlayer;
                    case "GraveyardOfPlayer":
                        return Tokens.TokenType.GraveyardOfPlayer;
                    case "DeckOfPlayer":
                        return Tokens.TokenType.DeckOfPlayer;
                    case "Hand":
                        return Tokens.TokenType.Hand;
                    case "Deck":
                        return Tokens.TokenType.Deck;
                    case "Field":
                        return Tokens.TokenType.Field;
                    case "Graveyard":
                        return Tokens.TokenType.Graveyard;
                    case "Owner":
                        return Tokens.TokenType.Owner;
                    case "Find":
                        return Tokens.TokenType.Find;
                    case "Push":
                        return Tokens.TokenType.Push;
                    case "SendBottom":
                        return Tokens.TokenType.SendBottom;
                    case "Pop":
                        return Tokens.TokenType.Pop;
                    case "Remove":
                        return Tokens.TokenType.Remove;
                    case "Shuffle":
                        return Tokens.TokenType.Shuffle;
                    case "Type":
                        return Tokens.TokenType.Type;
                    case "Faction":
                        return Tokens.TokenType.Faction;
                    case "Power":
                        return Tokens.TokenType.Power;
                    case "Range":
                        return Tokens.TokenType.Range;
                    case "OnActivation":
                        return Tokens.TokenType.OnAction;
                    case "Selector":
                        return Tokens.TokenType.Selector;
                    case "Source":
                        return Tokens.TokenType.Source;
                    case "Single":
                        return Tokens.TokenType.Single;
                    case "Predicate":
                        return Tokens.TokenType.Predicate;
                    case "PostActivation":
                        return Tokens.TokenType.PostAction;
                    default:
                        return Tokens.TokenType.String;
                }
            }
        }
    }
}
