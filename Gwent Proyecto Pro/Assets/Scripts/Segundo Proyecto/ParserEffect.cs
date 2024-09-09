using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent_Create_Card_Expression;
using Gwent_Create_Card_Token;
using System;
using Gwent_Create_Card_ParameterValue;

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
                        case "Action":
                            ClassifyEffect();
                            effect.Action = ParseAction();
                            break;
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

        #region Parser Card Manipulation
        private Expression ParseCardManipulationAction()
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

            List<Expression> expressions = new List<Expression>();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                if (Check(Tokens.TokenType.For))
                {
                    Consume(Tokens.TokenType.For, "Expected 'for' after '{'");
                    Consume(Tokens.TokenType.Identifier, "Expected 'target' after 'for'");
                    Consume(Tokens.TokenType.In, "Expected 'in' after 'target'");
                    Consume(Tokens.TokenType.Identifier, "Expected 'targets' after 'in'");

                    Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after 'targets'");

                    while (!Check(Tokens.TokenType.LlaveClose))
                    {
                        var currentToken = Advance();

                        // Si el token es un identificador distinto de 'targets' o 'context'
                        if (currentToken.Type == Tokens.TokenType.Identifier &&
                            currentToken.Value != "target" &&
                            currentToken.Value != "context")
                        {
                            var identifier = currentToken.Value;

                            if (Check(Tokens.TokenType.Asignacion))
                            {
                                Advance(); // Consume '='

                                var leftToken = Consume(Tokens.TokenType.Identifier, "Expected 'target' or 'context' after '='");
                                if (leftToken.Value != "target" && leftToken.Value != "context")
                                {
                                    throw new Exception("Expected 'target' or 'context' after '=', found: " + leftToken.Value);
                                }

                                Consume(Tokens.TokenType.Punto, "Expected '.' after 'targets' or 'context'");
                                var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                                // Manejo de propiedades de 'targets'
                                if (leftToken.Value == "target")
                                {
                                    ParseTargetsProperty(propertyToken, expressions);
                                }
                                // Manejo de propiedades de 'context'
                                else if (leftToken.Value == "context")
                                {
                                    ParseContextProperty(propertyToken, expressions);
                                }

                                Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                            }
                            else if (Check(Tokens.TokenType.Punto))
                            {
                                Advance(); // Consume '.'

                                var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                                ParseTargetsProperty(propertyToken, expressions);

                                Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                            }
                            else
                            {
                                throw new Exception($"Unexpected token '{currentToken.Value}' in action block");
                            }
                        }
                        else if (currentToken.Type == Tokens.TokenType.Identifier &&
                                 (currentToken.Value == "target" || currentToken.Value == "context"))
                        {
                            Consume(Tokens.TokenType.Punto, "Expected '.' after 'target' or 'context'");
                            var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                            if (currentToken.Value == "target")
                            {
                                ParseTargetsProperty(propertyToken, expressions);
                            }
                            else if (currentToken.Value == "context")
                            {
                                ParseContextProperty(propertyToken, expressions);
                            }

                            Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                        }
                    }
                    Consume(Tokens.TokenType.LlaveClose, "Expected '}' after Body of the for of my Effect");
                    Consume(Tokens.TokenType.PuntoComa, "Expected ';' after '}'");
                }
                else
                {
                    var currentToken = Advance();

                    // Si el token es un identificador distinto de 'targets' o 'context'
                    if (currentToken.Type == Tokens.TokenType.Identifier &&
                        currentToken.Value != "target" &&
                        currentToken.Value != "context")
                    {
                        var identifier = currentToken.Value;

                        if (Check(Tokens.TokenType.Asignacion))
                        {
                            Advance(); // Consume '='

                            var leftToken = Consume(Tokens.TokenType.Identifier, "Expected 'target' or 'context' after '='");
                            if (leftToken.Value != "target" && leftToken.Value != "context")
                            {
                                throw new Exception("Expected 'targets' or 'context' after '=', found: " + leftToken.Value);
                            }

                            Consume(Tokens.TokenType.Punto, "Expected '.' after 'targets' or 'context'");
                            var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                            // Manejo de propiedades de 'targets'
                            if (leftToken.Value == "target")
                            {
                                ParseTargetsProperty(propertyToken, expressions);
                            }
                            // Manejo de propiedades de 'context'
                            else if (leftToken.Value == "context")
                            {
                                ParseContextProperty(propertyToken, expressions);
                            }

                            Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                        }
                        else if (Check(Tokens.TokenType.Punto))
                        {
                            Advance(); // Consume '.'

                            var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                            ParseTargetsProperty(propertyToken, expressions);

                            Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                        }
                        else
                        {
                            throw new Exception($"Unexpected token '{currentToken.Value}' in action block");
                        }
                    }
                    else if (currentToken.Type == Tokens.TokenType.Identifier &&
                             (currentToken.Value == "target" || currentToken.Value == "context"))
                    {
                        Consume(Tokens.TokenType.Punto, "Expected '.' after 'target' or 'context'");
                        var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                        if (currentToken.Value == "target")
                        {
                            ParseTargetsProperty(propertyToken, expressions);
                        }
                        else if (currentToken.Value == "context")
                        {
                            ParseContextProperty(propertyToken, expressions);
                        }

                        Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                    }
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' after Body of the Action of my Effect");

            return new Expression.BlockExpression(expressions); // Assuming you have a block expression to group them
        }
        #endregion

        #region Target Movement 
        private Expression ParseTargetMovementAction()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Action'");
            Consume(Tokens.TokenType.ParentesisOpen, "Expected '(' after ':'");
            // Validación de 'targets'
            var targetsToken = Consume(Tokens.TokenType.Identifier, "Expected 'target' after '('");
            if (targetsToken.Value != "target")
            {
                throw new Exception("Expected 'target' after '(', found: " + targetsToken.Value);
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

            List<Expression> expressions = new List<Expression>();

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var currentToken = Advance();

                // Si el token es un identificador distinto de 'targets' o 'context'
                if (currentToken.Type == Tokens.TokenType.Identifier &&
                    currentToken.Value != "target" &&
                    currentToken.Value != "context")
                {
                    var identifier = currentToken.Value;

                    if (Check(Tokens.TokenType.Asignacion))
                    {
                        Advance(); // Consume '='

                        var leftToken = Consume(Tokens.TokenType.Identifier, "Expected 'target' or 'context' after '='");
                        if (leftToken.Value != "target" && leftToken.Value != "context")
                        {
                            throw new Exception("Expected 'target' or 'context' after '=', found: " + leftToken.Value);
                        }

                        Consume(Tokens.TokenType.Punto, "Expected '.' after 'targets' or 'context'");
                        var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                        // Manejo de propiedades de 'targets'
                        if (leftToken.Value == "target")
                        {
                            ParseTargetsProperty(propertyToken, expressions);
                        }
                        // Manejo de propiedades de 'context'
                        else if (leftToken.Value == "context")
                        {
                            ParseContextProperty(propertyToken, expressions);
                        }

                        Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                    }
                    else if (Check(Tokens.TokenType.Punto))
                    {
                        Advance(); // Consume '.'

                        var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                        ParseTargetsProperty(propertyToken, expressions);

                        Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                    }
                    else
                    {
                        throw new Exception($"Unexpected token '{currentToken.Value}' in action block");
                    }
                }
                else if (currentToken.Type == Tokens.TokenType.Identifier &&
                         (currentToken.Value == "target" || currentToken.Value == "context"))
                {
                    Consume(Tokens.TokenType.Punto, "Expected '.' after 'target' or 'context'");
                    var propertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");

                    if (currentToken.Value == "target")
                    {
                        ParseTargetsProperty(propertyToken, expressions);
                    }
                    else if (currentToken.Value == "context")
                    {
                        ParseContextProperty(propertyToken, expressions);
                    }

                    Consume(Tokens.TokenType.PuntoComa, "Expected ';' after statement");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' after Body of the Action of my Effect");

            return new Expression.BlockExpression(expressions); // Assuming you have a block expression to group them
        }

        private void ParseTargetsProperty(Tokens propertyToken, List<Expression> expressions)
        {
            // Manejo de propiedades del Bloque 1
            switch (propertyToken.Value)
            {
                case "Push":
                case "SendBottom":
                case "Add":
                case "Remove":
                    Consume(Tokens.TokenType.ParentesisOpen, $"Expected '(' after '{propertyToken.Value}'");
                    var identifier = Consume(Tokens.TokenType.Identifier, $"Expected identifier inside '({propertyToken.Value})'").Value;
                    Consume(Tokens.TokenType.ParentisisClose, $"Expected ')' after identifier");
                    expressions.Add(new Expression.TargetPropertyExpression(propertyToken.Value, identifier));
                    break;
                case "Pop":
                case "Shuffle":
                case "Owner":
                    Consume(Tokens.TokenType.ParentesisOpen, $"Expected '(' after '{propertyToken.Value}'");
                    Consume(Tokens.TokenType.ParentisisClose, $"Expected ')' after '{propertyToken.Value}'");
                    expressions.Add(new Expression.TargetPropertyExpression(propertyToken.Value, null));
                    break;
                default:
                    throw new Exception($"Unexpected target property '{propertyToken.Value}'");
            }
        }

        private void ParseContextProperty(Tokens propertyToken, List<Expression> expressions)
        {
            // Manejo de propiedades del Bloque 2
            switch (propertyToken.Value)
            {
                case "TriggerPlayer":
                case "Board":
                case "HandOfPlayer":
                case "Hand":
                case "FieldOfPlayer":
                case "Field":
                case "Graveyard":
                case "DeckOfPlayer":
                case "Deck":
                    if (Check(Tokens.TokenType.ParentesisOpen))
                    {
                        Advance(); // Consume '('
                        var identifier = Consume(Tokens.TokenType.Identifier, $"Expected identifier inside '({propertyToken.Value})'").Value;
                        Consume(Tokens.TokenType.ParentisisClose, $"Expected ')' after identifier");
                        expressions.Add(new Expression.ContextPropertyExpression(propertyToken.Value, identifier));
                    }
                    else if (Check(Tokens.TokenType.Punto))
                    {
                        Advance(); // Consume '.'
                        var nextPropertyToken = Consume(Tokens.TokenType.Identifier, "Expected property after '.'");
                        ParseTargetsProperty(nextPropertyToken, expressions);
                    }
                    else
                    {
                        throw new Exception($"Unexpected token after context property '{propertyToken.Value}'");
                    }
                    break;
                default:
                    throw new Exception($"Unexpected context property '{propertyToken.Value}'");
            }
        }
        #endregion

        public int EvaluateAttributeModification(Expression expression, Dictionary<string, ParameterValue> parameters, int targetPower)
        {
            // Diccionario para variables locales
            var localContext = new Dictionary<string, int>();
            return EvaluateExpression(expression, parameters, localContext, targetPower);
        }

        private int EvaluateExpression(Expression expression, Dictionary<string, ParameterValue> parameters, Dictionary<string, int> localContext, int targetPower)
        {
            if (expression is Expression.BlockExpression blockExpression)
            {
                foreach (var expr in blockExpression.Expressions)
                {
                    targetPower = EvaluateExpression(expr, parameters, localContext, targetPower);
                }
            }
            else if (expression is Expression.AssignmentExpression assignmentExpression)
            {
                var value = EvaluateExpression(assignmentExpression.Right, parameters, localContext, targetPower);
                if (assignmentExpression.Left is Expression.IdentifierExpression identifier)
                {
                    if (identifier.Name == "target.Power")
                    {
                        targetPower = value;
                    }
                    else
                    {
                        // Asignación a una variable local
                        localContext[identifier.Name] = value;
                    }
                }
            }
            else if (expression is Expression.BinaryExpression binaryExpression)
            {
                var leftValue = EvaluateExpression(binaryExpression.Left, parameters, localContext, targetPower);
                var rightValue = EvaluateExpression(binaryExpression.Right, parameters, localContext, targetPower);

                switch (binaryExpression.Operator)
                {
                    case "+":
                        return leftValue + rightValue;
                    case "-":
                        return leftValue - rightValue;
                    case "*":
                        return leftValue * rightValue;
                    case "/":
                        return leftValue / rightValue;
                    case "+=":
                        leftValue += rightValue;
                        UpdateVariable(binaryExpression.Left, leftValue, localContext, ref targetPower);
                        return leftValue;
                    case "-=":
                        leftValue -= rightValue;
                        UpdateVariable(binaryExpression.Left, leftValue, localContext, ref targetPower);
                        return leftValue;
                    case "*=":
                        leftValue *= rightValue;
                        UpdateVariable(binaryExpression.Left, leftValue, localContext, ref targetPower);
                        return leftValue;
                    case "/=":
                        leftValue /= rightValue;
                        UpdateVariable(binaryExpression.Left, leftValue, localContext, ref targetPower);
                        return leftValue;
                    case "++":
                        leftValue++;
                        UpdateVariable(binaryExpression.Left, leftValue, localContext, ref targetPower);
                        return leftValue; // Retorna el valor incrementado
                    case "--":
                        leftValue--;
                        UpdateVariable(binaryExpression.Left, leftValue, localContext, ref targetPower);
                        return leftValue; // Retorna el valor decrementado
                    default:
                        throw new Exception("Unknown operator: " + binaryExpression.Operator);
                }
            }
            else if (expression is Expression.IdentifierExpression identifierExpression)
            {
                if (parameters.TryGetValue(identifierExpression.Name, out var parameterValue))
                {
                    return Convert.ToInt32(parameterValue.Value);
                }
                else if (localContext.TryGetValue(identifierExpression.Name, out var localValue))
                {
                    return localValue;
                }
                else if (identifierExpression.Name == "target.Power")
                {
                    return targetPower;
                }
                else
                {
                    throw new Exception("Unknown identifier: " + identifierExpression.Name);
                }
            }
            else if (expression is Expression.LiteralExpression literalExpression)
            {
                return literalExpression.Value;
            }
            else if (expression is Expression.WhileExpression whileExpression)
            {
                while (EvaluateBooleanExpression(whileExpression.Condition, parameters, localContext, targetPower))
                {
                    targetPower = EvaluateExpression(whileExpression.Body, parameters, localContext, targetPower);
                }
            }

            return targetPower;
        }

        private bool EvaluateBooleanExpression(Expression expression, Dictionary<string, ParameterValue> parameters, Dictionary<string, int> localContext, int targetPower)
        {
            if (expression is Expression.BinaryExpression binaryExpression)
            {
                var leftValue = EvaluateExpression(binaryExpression.Left, parameters, localContext, targetPower);
                var rightValue = EvaluateExpression(binaryExpression.Right, parameters, localContext, targetPower);

                switch (binaryExpression.Operator)
                {
                    case "<":
                        return leftValue < rightValue;
                    case ">":
                        return leftValue > rightValue;
                    case "==":
                        return leftValue == rightValue;
                    case "!=":
                        return leftValue != rightValue;
                    case "<=":
                        return leftValue <= rightValue;
                    case ">=":
                        return leftValue >= rightValue;
                    default:
                        throw new Exception("Unknown boolean operator: " + binaryExpression.Operator);
                }
            }

            throw new Exception("Expected a boolean expression, found: " + expression.GetType());
        }

        private void UpdateVariable(Expression expression, int newValue, Dictionary<string, int> localContext, ref int targetPower)
        {
            if (expression is Expression.IdentifierExpression identifier)
            {
                if (identifier.Name == "target.Power")
                {
                    targetPower = newValue;
                }
                else
                {
                    localContext[identifier.Name] = newValue;
                }
            }
        }

        public List<(string, string)> ExtractActionsFromExpression(Expression expression)
        {
            var actions = new List<(string, string)>();

            switch (expression)
            {
                case Expression.BlockExpression block:
                    foreach (var expr in block.Expressions)
                    {
                        actions.AddRange(ExtractActionsFromExpression(expr));
                    }
                    break;

                case Expression.TargetPropertyExpression targetProperty:
                    var action = DetermineAction(targetProperty.PropertyName);
                    var location = DetermineLocation(targetProperty.Identifier);
                    actions.Add((action, location));
                    break;

                case Expression.ContextPropertyExpression contextProperty:
                    var contextAction = DetermineAction(contextProperty.PropertyName);
                    var contextLocation = DetermineLocation(contextProperty.Identifier);
                    actions.Add((contextAction, contextLocation));
                    break;

                case Expression.AssignmentExpression assignment:
                    actions.AddRange(ExtractActionsFromExpression(assignment.Right));
                    break;

                case Expression.BinaryExpression binary:
                    actions.AddRange(ExtractActionsFromExpression(binary.Left));
                    actions.AddRange(ExtractActionsFromExpression(binary.Right));
                    break;

                case Expression.LiteralExpression:
                case Expression.StringLiteralExpression:
                    // No actions to process for literals or strings
                    break;

                default:
                    Console.WriteLine($"Unsupported expression type encountered: {expression.GetType().Name}");
                    throw new Exception($"Unsupported expression type: {expression.GetType().Name}");
            }

            return actions;
        }

        private string DetermineAction(string property)
        {
            // Maps property names to actions
            return property switch
            {
                "Pop" => "Pop",
                "Add" => "Add",
                "SendBottom" => "SendBottom",
                "Remove" => "Remove",
                "Shuffle" => "Shuffle",
                _ => "Unknown"
            };
        }

        private string DetermineLocation(string identifier)
        {
            // Maps identifiers to locations
            return identifier switch
            {
                "TriggerPlayer" => "TriggerPlayer",
                "Board" => "Board",
                "Hand" => "HandOfPlayer(context.TriggerPlayer)",
                "Field" => "FieldOfPlayer(context.TriggerPlayer)",
                "Graveyard" => "GraveyardOfPlayer(context.TriggerPlayer)",
                "Deck" => "DeckOfPlayer(context.TriggerPlayer)",
                _ => identifier
            };
        }

        public List<(string, string)> MyInstrucTions(List<(string, string)> firstInstrucciones, List<Tokens> myTokens)
        {
            List<(string, string)> instrucciones = new List<(string, string)>();

            foreach ((string, string) elements in firstInstrucciones)
            {
                for (int i = 0; i < myTokens.Count; i++)
                {
                    if (myTokens[i].Value == elements.Item1)
                        instrucciones.Add((elements.Item1, myTokens[i - 2].Value));
                }
            }

            return instrucciones;
        }
    }
}
