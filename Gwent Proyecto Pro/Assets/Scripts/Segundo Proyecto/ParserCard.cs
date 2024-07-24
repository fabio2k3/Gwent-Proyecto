using Gwent_Create_Card_ActivatedEffect;
using Gwent_Create_Card_EffectDeclaration;
using Gwent_Create_Card_Expression;
using Gwent_Create_Card_ParameterValue;
using Gwent_Create_Card_PostAction;
using Gwent_Create_Card_Selector;
using Gwent_Create_Card_Token;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_ParserCard
{
    public class ParserCard : Parser
    {
        private Card card;

        public ParserCard(List<Tokens> tokens) : base(tokens)
        {
            this.card = new Card();
        }

        public Card ParseCard()
        {
            // Verificar el Inicio de la declaracion de mi Carta
            while (!IsAtEnd())
            {
                Tokens token = Advance();
                if (token.Type == Tokens.TokenType.Identifier && token.Value == "card")
                {
                    Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after 'card'");
                    break;
                }
            }

            // Asignar los Valores a card
            while (!IsAtEnd())
            {
                Tokens token = Advance();
                if (token.Type == Tokens.TokenType.LlaveClose)
                    break;

                if (token.Type == Tokens.TokenType.Identifier)
                {
                    switch (token.Value)
                    {
                        case "Type":
                            card.Type = ParseType();
                            break;
                        case "Name":
                            card.Name = ParseName();
                            break;
                        case "Faction":
                            card.Faction = ParseFaction();
                            break;
                        case "Power":
                            card.Power = ParsePower();
                            break;
                        case "Range":
                            card.Range = ParseRange();
                            break;
                        case "OnActivation":
                            //card.OnActivation = ParseOnActivation();
                            break;
                        default:
                            throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                    }
                }
            }

            return card;
        }

        // Parsear el Tipo de la Carta (Type)
        private string ParseType()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Type'");
            string type = Consume(Tokens.TokenType.String, "Expected type string").Value.Trim();
            Consume(Tokens.TokenType.Coma, "Expected ',' after type");

            if (type != "Oro" && type != "Plata" && type != "Clima" && type != "Aumento" && type != "Líder")
            {
                throw new Exception($"Invalid Type: {type}");
            }

            return type;
        }

        // Parsear la Faccion de la Carta (Faction)
        private string ParseFaction()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Faction'");
            string faction = Consume(Tokens.TokenType.String, "Expected faction string").Value.Trim();
            Consume(Tokens.TokenType.Coma, "Expected ',' after faction");

            if (faction != "Warrior" && faction != "Caballero" && faction != "Orc" && faction != "Orco")
            {
                throw new Exception($"Invalid Faction: {faction}");
            }

            return faction;
        }

        // Parsear el Power de la Carta
        private int ParsePower()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Power'");
            var expr = ParseExpression();
            Consume(Tokens.TokenType.Coma, "Expected ',' after power");

            return Evaluate(expr);
        }

        // Parsear los Rangos de la carta (Range)
        private List<string> ParseRange()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Range'");
            Consume(Tokens.TokenType.CorcheteOpen, "Expected '[' to start range list");
            var ranges = new List<string>();

            while (!Check(Tokens.TokenType.CorcheteClose))
            {
                string range = Consume(Tokens.TokenType.String, "Expected range string").Value.Trim();
                if (range != "Melee" && range != "Ranged" && range != "Siege")
                {
                    throw new Exception($"Invalid Range: {range}");
                }
                ranges.Add(range);

                if (!Check(Tokens.TokenType.CorcheteClose))
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between ranges");
                }
            }

            Consume(Tokens.TokenType.CorcheteClose, "Expected ']' to end range list");
            Consume(Tokens.TokenType.Coma, "Expected ',' after range list");

            return ranges;
        }

        #region Expresion
        // Parsear una Expresion
        private Expression ParseExpression()
        {
            return ParseTerm();
        }

        // Maneja los Operadores de Suma y Resta
        private Expression ParseTerm()
        {
            Expression expr = ParseFactor();

            while (Match(Tokens.TokenType.Plus, Tokens.TokenType.Menos))
            {
                string op = Previous().Value;
                Expression right = ParseFactor();
                expr = new Expression.BinaryExpression(expr, op, right);
            }

            return expr;
        }

        // Maneja los Operadores de Multiplicacion y Division
        private Expression ParseFactor()
        {
            Expression expr = ParseUnary();

            while (Match(Tokens.TokenType.Multi, Tokens.TokenType.Division))
            {
                string op = Previous().Value;
                Expression right = ParseUnary();
                expr = new Expression.BinaryExpression(expr, op, right);
            }

            return expr;
        }

        // Parsear Expresiones Unarias
        private Expression ParseUnary()
        {
            if (Match(Tokens.TokenType.Menos))
            {
                string op = Previous().Value;
                Expression right = ParsePrimary();
                return new Expression.BinaryExpression(new Expression.LiteralExpression(0), op, right);
            }

            return ParsePrimary();
        }

        // Parsear Expresiones Primarias
        private Expression ParsePrimary()
        {
            if (Match(Tokens.TokenType.Number))
            {
                return new Expression.LiteralExpression(int.Parse(Previous().Value));
            }

            if (Match(Tokens.TokenType.ParentesisOpen))
            {
                Expression expr = ParseExpression();
                Consume(Tokens.TokenType.ParentisisClose, "Expected ')' after expression");
                return expr;
            }

            throw new Exception("Expected expression");
        }

        // Verificar si el Token Coincide con algunos de los Especificaods
        private bool Match(params Tokens.TokenType[] types)
        {
            foreach (Tokens.TokenType type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }

            return false;
        }
        
        // Evaluar el Resultado
        private int Evaluate(Expression expr)
        {
            if (expr is Expression.LiteralExpression literal)
            {
                return literal.Value;
            }
            else if (expr is Expression.BinaryExpression binary)
            {
                int left = Evaluate(binary.Left);
                int right = Evaluate(binary.Right);

                switch (binary.Operator)
                {
                    case "+":
                        return left + right;
                    case "-":
                        return left - right;
                    case "*":
                        return left * right;
                    case "/":
                        if (right == 0) throw new DivideByZeroException("Division by zero");
                        return left / right;
                    case "^":
                        return (int)Math.Pow(left, right);
                    default:
                        throw new Exception($"Unknown operator {binary.Operator}");
                }
            }

            throw new Exception("Unknown expression type");
        }
        #endregion

        #region OnActivation
        private List<ActivatedEffect> ParseOnActivation()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'OnActivation'");
            Consume(Tokens.TokenType.CorcheteOpen, "Expected '[' to start OnActivation list");

            var effects = new List<ActivatedEffect>();

            while (!Check(Tokens.TokenType.CorcheteClose))
            {
                effects.Add(ParseActivatedEffect());

                if (!Check(Tokens.TokenType.CorcheteClose))
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between OnActivation effects");
                }
            }

            Consume(Tokens.TokenType.CorcheteClose, "Expected ']' to end OnActivation list");
            return effects;
        }

        private ActivatedEffect ParseActivatedEffect()
        {
            Consume(Tokens.TokenType.LlaveOpen, "Expected '{' to start ActivatedEffect");

            var activatedEffect = new ActivatedEffect();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var token = Advance();
                switch (token.Value)
                {
                    case "Effect":
                        activatedEffect.Effect = ParseEffectDeclaration();
                        break;
                    case "Selector":
                        activatedEffect.Selector = ParseSelector();
                        break;
                    case "PostAction":
                        activatedEffect.PostAction = ParsePostAction();
                        break;
                    default:
                        throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "sExpected '}' to end ActivatedEffect");
            return activatedEffect;
        }

        private EffectDeclaration ParseEffectDeclaration()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Effect'");
            Consume(Tokens.TokenType.LlaveOpen, "Expected '{' to start Effect");

            var effect = new EffectDeclaration();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var token = Advance();
                string key = token.Value;

                Consume(Tokens.TokenType.DoblePunto, $"Expected ':' after '{key}'");
                var valueToken = Advance();

                ParameterValue parameterValue;
                switch (valueToken.Type)
                {
                    case Tokens.TokenType.String:
                        parameterValue = new ParameterValue(ParameterType.String, valueToken.Value);
                        break;
                    case Tokens.TokenType.Number:
                        parameterValue = new ParameterValue(ParameterType.Number, int.Parse(valueToken.Value));
                        break;
                    case Tokens.TokenType.Identifier:
                        if (valueToken.Value == "true" || valueToken.Value == "false")
                        {
                            parameterValue = new ParameterValue(ParameterType.Boolean, bool.Parse(valueToken.Value));
                        }
                        else
                        {
                            throw new Exception($"Unexpected identifier {valueToken.Value} at line {valueToken.Row}");
                        }
                        break;
                    default:
                        throw new Exception($"Unexpected token type {valueToken.Type} at line {valueToken.Row}");
                }

                effect.Params[key] = parameterValue;

                if (!Check(Tokens.TokenType.LlaveClose))
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between parameters");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' to end Effect");
            return effect;
        }

        private Selector ParseSelector()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Selector'");
            Consume(Tokens.TokenType.LlaveOpen, "Expected '{' to start Selector");

            var selector = new Selector();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var token = Advance();
                switch (token.Value)
                {
                    case "Source":
                        selector.Source = ParseSource();
                        break;
                    case "Single":
                        selector.Single = ParseSingle();
                        break;
                    //case "Predicate":
                    //    selector.Predicate = ParsePredicate();
                    //    break;
                    default:
                        throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' to end Selector");
            return selector;
        }

        private string ParseSource()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Source'");
            string source = Consume(Tokens.TokenType.String, "Expected source string").Value.Trim();

            var validSources = new List<string> { "board", "hand", "otherHand", "deck", "otherDeck", "field", "otherField", "parent" };
            if (!validSources.Contains(source))
            {
                throw new Exception($"Invalid Source: {source}");
            }

            return source;
        }

        private string ParseSingle()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Single'");

            var token = Advance();
            if (token.Value != "true" && token.Value != "false")
            {
                throw new Exception($"Invalid value '{token.Value}' for 'Single' at line {token.Row}. Expected 'true' or 'false'");
            }

            var singleValue = token.Value;

            Consume(Tokens.TokenType.Coma, "Expected ',' after 'Single'");

            return singleValue;
        }

        // REVISAR
        private string ParsePredicate()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Predicate'");
            return Consume(Tokens.TokenType.String, "Expected predicate string").Value;
        }

        private PostAction ParsePostAction()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'PostAction'");
            Consume(Tokens.TokenType.LlaveOpen, "Expected '{' to start PostAction");

            var postAction = new PostAction();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var token = Advance();
                switch (token.Value)
                {
                    case "Type":
                        postAction.Type = ParseName();
                        break;
                    case "Selector":
                        postAction.Selector = ParseSelector();
                        break;
                    default:
                        throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' to end PostAction");
            return postAction;
        }
        #endregion
    }
}