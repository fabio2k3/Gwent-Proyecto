using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent_Create_Card_Token;
using System.Text;

namespace Gwent_Create_Card_Lexer
{
    public class Lexer
    {
        public static string[] specialCaracter = { "+", "-", "*", "/", "^", "{", "}", "(", ")", "|", "\"", "@", ";", ":", "=", "<", ">" };

        private static Dictionary<string, Tokens.TokenType> keyWordsOfDSL = new Dictionary<string, Tokens.TokenType> {
            { "effect", Tokens.TokenType.effect},
            { "Name", Tokens.TokenType.Name},
            { "Params", Tokens.TokenType.Params },
            { "Action", Tokens.TokenType.Action},
            { "TriggerPlayer", Tokens.TokenType.TriggerPlayer},
            { "Board",Tokens.TokenType.Board },
            { "HandOfPlayer", Tokens.TokenType.HandOfPlayer},
            { "Hand", Tokens.TokenType.Hand  },
            { "FieldOfPlayer", Tokens.TokenType.FieldOfPlayer },
            { "Field",  Tokens.TokenType.Field},
            { "GraveyardOfPlayer", Tokens.TokenType.GraveyardOfPlayer },
            { "Graveyard", Tokens.TokenType.Graveyard },
            { "DeckOfPlayer", Tokens.TokenType.DeckOfPlayer},
            { "Find", Tokens.TokenType.Find },
            { "Push", Tokens.TokenType.Push },
            { "SendBottom", Tokens.TokenType.SendBottom },
            { "Pop", Tokens.TokenType.Pop },
            { "Remove", Tokens.TokenType.Remove},
            { "Shuffle", Tokens.TokenType.Shuffle},
            { "card", Tokens.TokenType.card  },
            { "Type",  Tokens.TokenType.Type},
            { "Faction", Tokens.TokenType.Faction},
            { "Power", Tokens.TokenType.Power },
            { "Range", Tokens.TokenType.Range},
            { "OnActivation", Tokens.TokenType.OnActivation },
            { "Effect", Tokens.TokenType.Effect },
            { "Selector",  Tokens.TokenType.Selector},
            { "Source",  Tokens.TokenType.Source},
            { "Single", Tokens.TokenType.Single},
            { "Predicate", Tokens.TokenType.Predicate },
            { "PostAction", Tokens.TokenType.PostAction},
        };
        public static List<(string, int)> GetWordsAndRow(string input, string[] special)
        {
            List<(string, int)> tokensAndLines = new List<(string, int)>();

            int line = 1;

            string newInput = Lexer.Spaces(input, specialCaracter);

            for (int i = 0; i < newInput.Length; i++)
            {
                char c = newInput[i];
                string charString = c.ToString();

                if (Array.Exists(specialCaracter, element => element == charString))
                    tokensAndLines.Add((charString, line));
                else if (!char.IsWhiteSpace(c))
                {
                    string word = "";
                    while (i < newInput.Length && !char.IsWhiteSpace(newInput[i]) && !Array.Exists(specialCaracter, element => element == newInput[i].ToString()))
                    {
                        word += newInput[i];
                        i++;
                    }
                    tokensAndLines.Add((word, line));
                    i--;
                }
                if (c == '\n')
                    line++;
            }

            return tokensAndLines;
        }

        private static string Spaces(string input, string[] specialCaracter)
        {
            StringBuilder sb = new StringBuilder();
            bool lastSpace = false;

            foreach (char c in input)
            {
                if (Array.Exists(specialCaracter, element => element == c.ToString()))
                {
                    if (!lastSpace)
                    {
                        sb.Append(' ');
                    }
                    sb.Append(c);
                    sb.Append(' ');
                    lastSpace = true;
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (c == '\n' || c == '\r')
                    {
                        sb.Append(c);
                        lastSpace = false;
                    }
                    else if (!lastSpace)
                    {
                        sb.Append(' ');
                        lastSpace = true;
                    }
                }
                else
                {
                    sb.Append(c);
                    lastSpace = false;
                }
            }

            string result = sb.ToString().Replace("  ", " ");

            return result.Trim();
        }

        public static List<Tokens> GetTokens(List<(string, int)> wordsAndRow)
        {
            List<Tokens> tokens = new List<Tokens>();

            for (int i = 0; i < wordsAndRow.Count; i++)
            {
                string token = wordsAndRow[i].Item1;
                int row = wordsAndRow[i].Item2;

                if (Lexer.keyWordsOfDSL.ContainsKey(token))
                    tokens.Add(new Tokens(token, Lexer.keyWordsOfDSL[token], row));
                else if (token == "+")
                {
                    if (wordsAndRow[i + 1].Item1 == "+")
                    {
                        tokens.Add(new Tokens("++", Tokens.TokenType.PlusPlus, row));
                        i += 1;
                    }
                    else if (wordsAndRow[i + 1].Item1 == "=")
                    {
                        tokens.Add(new Tokens("+=", Tokens.TokenType.PlusFunc, row));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens("+", Tokens.TokenType.Plus, row));
                }
                else if (token == "-")
                {
                    if (wordsAndRow[i + 1].Item1 == "-")
                    {
                        tokens.Add(new Tokens("--", Tokens.TokenType.MenosMenos, row));
                        i += 1;
                    }
                    else if (wordsAndRow[i + 1].Item1 == "=")
                    {
                        tokens.Add(new Tokens("-=", Tokens.TokenType.MenosFunc, row));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens("-", Tokens.TokenType.Menos, row));
                }
                else if (token == "&")
                {
                    if (wordsAndRow[i + 1].Item1 == "&")
                    {
                        tokens.Add(new Tokens("&&", Tokens.TokenType.And, row));
                        i += 1;
                    }

                    throw new Exception($"Error en la línea {row}, & no está definido en DSL");
                }
                else if (token == "|")
                {
                    if (wordsAndRow[i + 1].Item1 == "|")
                    {
                        tokens.Add(new Tokens("||", Tokens.TokenType.Or, row));
                        i += 1;
                    }

                    throw new Exception($"Error en la línea {row}, | no está definido en DSL");
                }
                else if (token == "<")
                {
                    if (wordsAndRow[i + 1].Item1 == "=")
                    {
                        tokens.Add(new Tokens("<=", Tokens.TokenType.MenorIgualQ, row));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens("<", Tokens.TokenType.MenorQ, row));
                }
                else if (token == ">")
                {
                    if (wordsAndRow[i + 1].Item1 == "=")
                    {
                        tokens.Add(new Tokens(">=", Tokens.TokenType.MayorIgualQ, row));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens(">", Tokens.TokenType.MayorQ, row));
                }
                else if (token == "@")
                {
                    if (wordsAndRow[i + 1].Item1 == "@")
                    {
                        tokens.Add(new Tokens("@@", Tokens.TokenType.DobleConcatenacion, row));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens("@", Tokens.TokenType.Concatenacion, row));
                }
                else if (token == "=")
                {
                    if (wordsAndRow[i + 1].Item1 == "=")
                    {
                        tokens.Add(new Tokens("==", Tokens.TokenType.Igual, row));
                        i += 1;
                    }
                    else if (wordsAndRow[i + 1].Item1 == ">")
                    {
                        tokens.Add(new Tokens("=>", Tokens.TokenType.Arrow, row));
                        i += 1;
                    }
                    else
                        tokens.Add(new Tokens("=", Tokens.TokenType.Asignacion, row));
                }
                else
                    tokens.Add(new Tokens(token, Lexer.ClassifyTokens(token), row));
            }

            return tokens;
        }

        private static Tokens.TokenType ClassifyTokens(string token)
        {
            try
            {
                int.Parse(token);
                return Tokens.TokenType.Number;

            }
            catch (Exception)
            {
                switch (token)
                {
                    case "*":
                        return Tokens.TokenType.Multi;
                    case "/":
                        return Tokens.TokenType.Division;
                    case "^":
                        return Tokens.TokenType.Potencia;
                    case "for":
                        return Tokens.TokenType.For;
                    case "while":
                        return Tokens.TokenType.While;
                    case ".":
                        return Tokens.TokenType.Punto;
                    case ";":
                        return Tokens.TokenType.PuntoComa;
                    case ":":
                        return Tokens.TokenType.DoblePunto;
                    case ",":
                        return Tokens.TokenType.Coma;
                    case "(":
                        return Tokens.TokenType.ParentesisOpen;
                    case ")":
                        return Tokens.TokenType.ParentisisClose;
                    case "{":
                        return Tokens.TokenType.LlaveOpen;
                    case "}":
                        return Tokens.TokenType.LlaveClose;
                    case "\"":
                        return Tokens.TokenType.Comillas;
                    default:
                        return Tokens.TokenType.Identifier;
                }

            }
        }
    }
}
