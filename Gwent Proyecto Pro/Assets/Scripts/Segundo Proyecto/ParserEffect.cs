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

        public Effect ParseEffect()
        {
            // Avanzar hasta encontrar "effect" y una llave abierta
            while (!IsAtEnd())
            {
                Tokens token = Advance();
                if (token.Type == Tokens.TokenType.Identifier && token.Value == "effect")
                {
                    Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after 'effect'");
                    break;
                }
            }

            // Procesar las secciones dentro de la llave
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
