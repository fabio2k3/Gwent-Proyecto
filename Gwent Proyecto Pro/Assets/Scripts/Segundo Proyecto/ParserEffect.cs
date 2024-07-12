using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent_Create_Card_Expression;
using Gwent_Create_Card_Token;
using System;

namespace Gwent_Create_Card_ParserEffect
{
    public class ParserEffect : Parser
    {
        private Effect effect;

        public ParserEffect(List<Tokens> tokens) : base(tokens)
        {
            this.effect = new Effect();
        }

        // Metodo que se encarga de Parsear el Efecto
        public Effect ParseEffect()
        {
            // Verificar el Inicio de mi Efecto
            while (!IsAtEnd())
            {
                Tokens token = Advance();
                if (token.Type == Tokens.TokenType.Identifier && token.Value == "effect")
                {
                    Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after 'effect'");
                    break;
                }
            }

            // Asignar los Valores a effect
            while (!IsAtEnd())
            {
                Tokens token = Advance();
                if (token.Type == Tokens.TokenType.LlaveClose)
                    break;

                if (token.Type == Tokens.TokenType.Identifier)
                {
                    switch (token.Value)
                    {
                        case "Name":
                            effect.Name = ParseName();
                            break;
                        case "Params":
                            effect.Params = ParseParams();
                            break;
                        default:
                            throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                    }
                }
            }

            return effect;
        }
    }
}
