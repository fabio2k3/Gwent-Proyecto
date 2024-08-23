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
                        //case "Action":
                        //    ClassifyEffect();
                        //    effect.Action = ParseAction();
                        //    break;
                        default:
                            throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                    }
                }
            }

            // Clasificar el efecto después de haber parseado los parámetros




            return effect;
        }

        private void ClassifyEffect()
        {
            var attributeModificationTokens = new HashSet<Tokens.TokenType> { Tokens.TokenType.Identifier }; // Ajusta según los tokens específicos
            var cardManipulationTokens = new HashSet<Tokens.TokenType> { Tokens.TokenType.Identifier }; // Ajusta según los tokens específicos
            var targetMovementTokens = new HashSet<Tokens.TokenType> { Tokens.TokenType.Identifier }; // Ajusta según los tokens específicos

            bool hasAttributeModification = false;
            bool hasCardManipulation = false;
            bool hasTargetMovement = false;

            // Recorre los tokens analizados para determinar la clasificación
            foreach (var token in tokens)
            {
                if (token.Type == Tokens.TokenType.Identifier)
                {
                    var value = token.Value;

                    if (attributeModificationTokens.Contains(token.Type) &&
                        (value == "Amount" || value == "Damage" || value == "Increase" || value == "Decrease"))
                    {
                        hasAttributeModification = true;
                    }
                    else if (cardManipulationTokens.Contains(token.Type) &&
                             (value == "Draw" || value == "Discard" || value == "Add" || value == "Remove"))
                    {
                        hasCardManipulation = true;
                    }
                    else if (targetMovementTokens.Contains(token.Type) &&
                             (value == "Move" || value == "Transfer" || value == "Shuffle"))
                    {
                        hasTargetMovement = true;
                    }
                }
            }

            if (hasAttributeModification)
            {
                effect.Classification = "AttributeModification";
            }
            else if (hasCardManipulation)
            {
                effect.Classification = "CardManipulation";
            }
            else if (hasTargetMovement)
            {
                effect.Classification = "TargetMovement";
            }
            else
            {
                effect.Classification = "Unknown";
            }
        }

        private Expression ParseAction()
        {
            // Verificar la clasificación y parsear la acción en consecuencia
            switch (effect.Classification)
            {
                case "AttributeModification":
                    return ParseAttributeModificationAction();
                case "CardManipulation":
                    return ParseCardManipulationAction();
                case "TargetMovement":
                    return ParseTargetMovementAction();
                default:
                    throw new Exception($"Unknown effect classification: {effect.Classification}");
            }
        }

        #region Parse Attribute Modification
        private Expression ParseAttributeModificationAction()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Action'");
            Consume(Tokens.TokenType.ParentesisOpen, "Expected '(' after ':'");

            // Validación de 'targets'
            var targetsToken = Consume(Tokens.TokenType.Identifier, "Expected 'targets' after '('");
            if (targetsToken.Value != "targets")
            {
                throw new Exception("Expected 'targets' after '(', found: " + targetsToken.Value);
            }

            Consume(Tokens.TokenType.Coma, "Expected ',' after 'targets'");

            // Validación de 'context'
            var contextToken = Consume(Tokens.TokenType.Identifier, "Expected 'context' after ','");
            if (contextToken.Value != "context")
            {
                throw new Exception("Expected 'context' after ',', found: " + contextToken.Value);
            }

            Consume(Tokens.TokenType.ParentisisClose, "Expected ')' after 'context'");
            Consume(Tokens.TokenType.Arrow, "Expected '=>' after ')'");
            Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after '=>'");

            // Lista para almacenar expresiones
            List<Expression> expressions = new List<Expression>();

            // Verificar si el contenido comienza con un 'for'
            if (Check(Tokens.TokenType.For))
            {
                // Consumo de tokens del bucle for
                Consume(Tokens.TokenType.For, "Expected 'for' after '{'");
                var targetToken = Consume(Tokens.TokenType.Identifier, "Expected 'target' after 'for'");
                if (targetToken.Value != "target")
                {
                    throw new Exception("Expected 'target' after 'for', found: " + targetToken.Value);
                }

                Consume(Tokens.TokenType.In, "Expected 'in' after 'target'");
                var inTargetsToken = Consume(Tokens.TokenType.Identifier, "Expected 'targets' after 'in'");
                if (inTargetsToken.Value != "targets")
                {
                    throw new Exception("Expected 'targets' after 'in', found: " + inTargetsToken.Value);
                }

                Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after 'targets'");

                while (!Check(Tokens.TokenType.LlaveClose))
                {
                    ParseStatement(expressions);
                }

                Consume(Tokens.TokenType.LlaveClose, "Expected '}' after Body of the for of my Effect");
                Consume(Tokens.TokenType.PuntoComa, "Expected ';' after '}'");
            }
            else
            {
                // Si no comienza con un 'for', puede ser otro tipo de expresión
                while (!Check(Tokens.TokenType.LlaveClose))
                {
                    ParseStatement(expressions);
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' after Body of the Action of my Effect");

            // Devuelve una lista de expresiones que se pueden evaluar en el futuro
            return new Expression.BlockExpression(expressions);
        }

        private void ParseStatement(List<Expression> expressions)
        {
            if (Check(Tokens.TokenType.Identifier))
            {
                var identifier = Advance();
                if (Check(Tokens.TokenType.Asignacion))
                {
                    Advance(); // Consume '='
                    var rightExpr = ParseExpression();

                    // Verificamos si el identificador es una variable y la tratamos como tal
                    expressions.Add(new Expression.AssignmentExpression(
                        new Expression.IdentifierExpression(identifier.Value),
                        rightExpr
                    ));

                    Consume(Tokens.TokenType.PuntoComa, "Expected ';' after assignment");
                }
                else if (Check(Tokens.TokenType.PlusPlus) || Check(Tokens.TokenType.MenosMenos))
                {
                    var operation = Advance(); // Consume '++' o '--'
                    expressions.Add(new Expression.BinaryExpression(
                        new Expression.IdentifierExpression(identifier.Value),
                        operation.Value,
                        new Expression.LiteralExpression(1)
                    ));

                    Consume(Tokens.TokenType.PuntoComa, "Expected ';' after increment/decrement");
                }
                else
                {
                    throw new Exception("Expected '=' or '++/--' after identifier, found: " + Peek().Value);
                }
            }
            else if (Check(Tokens.TokenType.While))
            {
                Advance(); // Consume 'while'
                Consume(Tokens.TokenType.ParentesisOpen, "Expected '(' after 'while'");

                var leftExpr = ParseExpression();
                var operatorToken = ConsumeBooleanOperator(); // Consume operador booleano
                var rightExpr = ParseExpression();

                Consume(Tokens.TokenType.ParentisisClose, "Expected ')' after boolean expression");

                // Parseo de la expresión dentro del while
                var target = Consume(Tokens.TokenType.Identifier, "Expected 'target' after 'while'");
                Consume(Tokens.TokenType.Punto, "Expected '.' after 'target'");
                var power = Consume(Tokens.TokenType.Identifier, "Expected 'Power' after 'target.'");

                var operation = ConsumeOperation();
                var valueExpr = ParseExpression();

                expressions.Add(new Expression.WhileExpression(
                    new Expression.BinaryExpression(leftExpr, operatorToken.Value, rightExpr),
                    new Expression.AssignmentExpression(
                        new Expression.IdentifierExpression(target.Value + "." + power.Value),
                        new Expression.BinaryExpression(
                            new Expression.IdentifierExpression(target.Value + "." + power.Value),
                            operation.Value,
                            valueExpr
                        )
                    )
                ));

                Consume(Tokens.TokenType.PuntoComa, "Expected ';' after while loop body");
            }
            else
            {
                throw new Exception("Unexpected token in the action block: " + Peek().Value);
            }
        }

        // Métodos auxiliares
        private Expression ParseExpression()
        {
            if (Check(Tokens.TokenType.Identifier))
            {
                var identifier = Advance();
                if (Check(Tokens.TokenType.PlusPlus) || Check(Tokens.TokenType.MenosMenos))
                {
                    var operation = Advance(); // Consume '++' o '--'
                    return new Expression.BinaryExpression(
                        new Expression.IdentifierExpression(identifier.Value),
                        operation.Value,
                        new Expression.LiteralExpression(1)
                    );
                }
                return new Expression.IdentifierExpression(identifier.Value);
            }
            else if (Check(Tokens.TokenType.Number))
            {
                return new Expression.LiteralExpression(int.Parse(Advance().Value));
            }
            else if (Check(Tokens.TokenType.ParentesisOpen))
            {
                Advance(); // Consume '('
                var leftExpr = ParseExpression();
                var operation = ConsumeOperation(); // Consume la operación matemática
                var rightExpr = ParseExpression();
                Consume(Tokens.TokenType.ParentisisClose, "Expected ')' after expression");

                return new Expression.BinaryExpression(leftExpr, operation.Value, rightExpr);
            }

            throw new Exception("Expected an expression, found: " + Peek().Value);
        }

        private Tokens ConsumeBooleanOperator()
        {
            if (Check(Tokens.TokenType.MenorQ) || Check(Tokens.TokenType.MenorIgualQ) || Check(Tokens.TokenType.MayorQ) || Check(Tokens.TokenType.MayorIgualQ) || Check(Tokens.TokenType.Igual))
            {
                return Advance();
            }

            throw new Exception("Expected a boolean operator, found: " + Peek().Value);
        }

        private Tokens ConsumeOperation()
        {
            if (Check(Tokens.TokenType.Plus) || Check(Tokens.TokenType.Menos) || Check(Tokens.TokenType.Multi) || Check(Tokens.TokenType.Division) || Check(Tokens.TokenType.PlusFunc) || Check(Tokens.TokenType.MenosFunc) || Check(Tokens.TokenType.MultiFunc) || Check(Tokens.TokenType.DivFunc) || Check(Tokens.TokenType.PlusPlus) || Check(Tokens.TokenType.MenosMenos))
            {
                return Advance();
            }

            throw new Exception("Expected a mathematical operation, found: " + Peek().Value);
        }
        #endregion

        private Expression ParseCardManipulationAction()
        {
            // Implementar el parsing específico para CardManipulation aquí
            // Por ejemplo, buscar tokens relacionados con manipulación de cartas
            throw new NotImplementedException("Error: CardManipulationAction");
        }

        private Expression ParseTargetMovementAction()
        {
            // Implementar el parsing específico para TargetMovement aquí
            // Por ejemplo, buscar tokens relacionados con movimiento de objetivos
            throw new NotImplementedException("Error: TargetMovementAction");
        }
    }
}
