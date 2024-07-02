using Gwent_Create_Card_Token;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_Expression
{
    public class Parser 
    {
        protected List<Tokens> tokens;
        protected int current = 0;

        public Parser(List<Tokens> tokens)
        {
            this.tokens = tokens;
        }

        // Métodos de utilidad para el parser
        protected Tokens Consume(Tokens.TokenType type, string message)
        {
            if (Check(type)) return Advance();
            throw new Exception(message);
        }

        protected bool Check(Tokens.TokenType type)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        protected Tokens Advance()
        {
            if (!IsAtEnd()) current++;
            return Previous();
        }

        protected bool IsAtEnd()
        {
            return Peek().Type == Tokens.TokenType.EOF;
        }

        protected Tokens Peek()
        {
            return tokens[current];
        }

        protected Tokens Previous()
        {
            return tokens[current - 1];
        }

        protected string ParseName()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Name'");
            string name = Consume(Tokens.TokenType.String, "Expected name string").Value;
            Consume(Tokens.TokenType.Coma, "Expected ',' after name");

            return name.Trim('"'); // Remove surrounding quotes from the name
        }

        protected Dictionary<string, string> ParseParams()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Params'");
            Consume(Tokens.TokenType.LlaveOpen, "Expected '{' to start params list");
            var parameters = new Dictionary<string, string>();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                string paramName = Consume(Tokens.TokenType.Identifier, "Expected parameter name").Value;
                Consume(Tokens.TokenType.DoblePunto, "Expected ':' after parameter name");
                string paramType = Consume(Tokens.TokenType.Identifier, "Expected parameter type").Value;

                if (paramType != "Number" && paramType != "String" && paramType != "Bool")
                {
                    throw new Exception($"Invalid parameter type: {paramType} at line {Previous().Row}");
                }
                parameters[paramName] = paramType;

                if (!Check(Tokens.TokenType.LlaveClose))
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between parameters");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' to end params list");
            return parameters;
        }
    }
}

