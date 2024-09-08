using Gwent_Create_Card_ActivatedEffect;
using Gwent_Create_Card_EffectDeclaration;
using Gwent_Create_Card_Expression;
using Gwent_Create_Card_ParameterValue;
using Gwent_Create_Card_PostAction;
using Gwent_Create_Card_Selector;
using Gwent_Create_Card_Token;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Gwent_Create_Card_Lexer;
using Gwent_Create_Card_ParserEffect;


namespace Gwent_Create_Card_ParserCard
{
    public class ParserCard : Parser
    {
        private Card card;

        private CheckDiccionarios effectCheck;

        private string pathTexts = "Assets/Textos/Texts Effects";


        public ParserCard(List<Tokens> tokens) : base(tokens)
        {
            this.card = new Card();
        }

        // Metodo Fundamental para la declaración de mi Carta
        public Card ParseCard()
        {
            // Avanzar por los tokens hasta encontrar 'card'
            while (!IsAtEnd())
            {
                Tokens token = Advance();
                if (token.Type == Tokens.TokenType.Identifier && token.Value == "card")
                {
                    Consume(Tokens.TokenType.LlaveOpen, "Expected '{' after 'card'");
                    break;
                }
            }

            // Analizar los tokens mientras no se encuentre el final de Linea
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
                            card.Type = ParseType(); // Parsear el Tipo de mi Carta
                            break;
                        case "Name":
                            card.Name = ParseName(); // Parsear el Nombre de mi Carta
                            break;
                        case "Faction":
                            card.Faction = ParseFaction(); // Parsear la Facion de mi Carta
                            break;
                        case "Power":
                            card.Power = ParsePower(); // Parsear el Ataque de mi Carta
                            break;
                        case "Range":
                            card.Range = ParseRange(); // Parsear las Posicions donde puede ser invocada mi Carta
                            break;
                        case "OnActivation":
                            card.OnActivation = ParseOnActivation(); // Parsear los efectos declardos en mi Carta
                            break;
                        default:
                            throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                    }
                }
            }

            return card;
        }

        #region Parse Type
        private string ParseType()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Type'");
            string type = Consume(Tokens.TokenType.String, "Expected type string").Value.Trim();
            Consume(Tokens.TokenType.Coma, "Expected ',' after type");

            // Validar el Tipo de mi Carta
            if (type != "Oro" && type != "Plata" && type != "Clima" && type != "Aumento" && type != "Líder")
            {
                throw new Exception($"Invalid Type: {type}");
            }

            return type;
        }
        #endregion


        #region Parse Faction
        private string ParseFaction()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Faction'");
            string faction = Consume(Tokens.TokenType.String, "Expected faction string").Value.Trim();
            Consume(Tokens.TokenType.Coma, "Expected ',' after faction");

            // Validar la Faccion de mi carta
            if (faction != "Warrior"  && faction != "Orc" )
            {
                throw new Exception($"Invalid Faction: {faction}");
            }

            return faction;
        }
        #endregion


        #region Parse Power
        private int ParsePower()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Power'");
            var expr = ParseExpression(); // Analizar la Expresion del Poder (Ataque) de mi carta
            Consume(Tokens.TokenType.Coma, "Expected ',' after power");

            return Evaluate(expr);
        }
        #endregion


        #region Parse Range
        private List<string> ParseRange()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Range'");
            Consume(Tokens.TokenType.CorcheteOpen, "Expected '[' to start range list");
            var ranges = new List<string>();

            // Analizar tods los string separods por coma
            while (!Check(Tokens.TokenType.CorcheteClose))
            {
                string range = Consume(Tokens.TokenType.String, "Expected range string").Value.Trim();

                // Validar mi String
                if (range != "Melee" && range != "Ranged" && range != "Siege")
                {
                    throw new Exception($"Invalid Range: {range}");
                }
                ranges.Add(range);

                // Garantizar que hay una coma si no se termina la lista
                if (!Check(Tokens.TokenType.CorcheteClose))
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between ranges");
                }
            }

            Consume(Tokens.TokenType.CorcheteClose, "Expected ']' to end range list");
            Consume(Tokens.TokenType.Coma, "Expected ',' after range list");

            return ranges;
        }
        #endregion


        #region Expresion

        // Metodo para iniciar el analisis de nuestra expresion Matematica
        private Expression ParseExpression()
        {
            return ParseTerm();
        }

        // Metodo para Anlizar las variables de una expresion con Suma y Resta
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

        // Metodo para Anlizar las variables de una expresion con Multiplicacion y Division
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

        // Analizar expresion unarias 
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

        // MEtodo para analizar valores de numeros y expressiones entre parentesis
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

        // Metodo para Evaluar la expression
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


        // Metodo para verificar si los tokens coinciden con algunos de los tipos establecidos
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

        #region OnActivation
        // Metodo para analizar los Efectos declarados en mi Carta 
        private List<ActivatedEffect> ParseOnActivation()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'OnActivation'");
            Consume(Tokens.TokenType.CorcheteOpen, "Expected '[' to start OnActivation list");

            var effects = new List<ActivatedEffect>();

            // Mientras no se cierre el corchete analizar todas las declaraciones de efecto
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
                    case "Effect": // Parsear Nombre y Paramaetros de mi Efecto
                        activatedEffect.Effect = ParseEffectDeclaration();
                        break;
                    case "Selector": // Parsear mi Selector
                        activatedEffect.Selector = ParseSelector();
                        break;
                    case "PostAction": // Parsear mi PostAction
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

            bool nameFound = false;
            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var token = Peek();

                // Tomar el Nombre de mi Efecto
                if (token.Value == "Name")
                {
                    Advance(); 
                    Consume(Tokens.TokenType.DoblePunto, $"Expected ':' after 'Name'");
                    var nameToken = Consume(Tokens.TokenType.String, "Expected effect name string"); // Token que corresponde al Nombre de mi efecto
                    effect.Name = nameToken.Value.Trim('"'); // asignar el valor
                    Console.WriteLine($"Debug: Effect Name = '{effect.Name}'"); 
                    nameFound = true;

                    if (Check(Tokens.TokenType.Coma))
                    {
                        Consume(Tokens.TokenType.Coma, "Expected ',' after effect name");
                    }
                    break;
                }
                else
                {
                    break;
                }
            }

            if (!nameFound)
            {
                throw new Exception("Effect declaration must contain a 'Name' property.");
            }

            while (!Check(Tokens.TokenType.LlaveClose))
            {
                var token = Advance();
                string key = token.Value;

                Consume(Tokens.TokenType.DoblePunto, $"Expected ':' after '{key}'");
                var valueToken = Advance();

                // Parsear los Parametros de mi Efecto
                ParameterValue parameterValue; 
                switch (valueToken.Type)
                {
                    // Caso de que sea un String
                    case Tokens.TokenType.String: 
                        parameterValue = new ParameterValue(ParameterType.String, valueToken.Value.Trim('"'));
                        break;
                    // Caso que sea un Numero
                    case Tokens.TokenType.Number: 
                        parameterValue = new ParameterValue(ParameterType.Number, int.Parse(valueToken.Value));
                        break;
                    // Caso de que sea un Booleano
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

                // Verificar si mi Efecto Declarado Existe y si los parametros coinciden
                Dictionary<string, string> effectParams = TakeParamsOfMyEffect(effect.Name);

                if(CheckDiccionarios.CheckDiccionary(effectParams, effect.Params))
                    effect.Params[key] = parameterValue;

                if (!Check(Tokens.TokenType.LlaveClose))
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between parameters");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' to end Effect");
            return effect;
        }


        // Metodo que se encarga de Iniciar el Parseo del Selector
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
                    case "Predicate":  
                        selector.Predicate = ParsePredicate();
                        break;
                    default:
                        throw new Exception($"Unexpected identifier {token.Value} at line {token.Row}");
                }
            }

            Consume(Tokens.TokenType.LlaveClose, "Expected '}' to end Selector");
            return selector;
        }


        // Parsear la lista de los lugares donde se ejecutara mi Efecto
        private List<string> ParseSource()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Source'");

            var sources = new List<string>();

            // String validos 
            var validSources = new HashSet<string> { "board", "hand", "otherHand", "deck", "otherDeck", "field", "otherField", "parent" };

            while (true)
            {
                string source = Consume(Tokens.TokenType.String, "Expected source string").Value.Trim('"').Trim();

                if (!validSources.Contains(source))
                {
                    throw new Exception($"Invalid Source: {source}");
                }
                sources.Add(source);

                if (Check(Tokens.TokenType.Coma)) // VAlidar la separacion por comas
                {
                    Consume(Tokens.TokenType.Coma, "Expected ',' between sources");
                    Console.WriteLine("Debug: Consumed ','"); 
                }
                else
                {
                    break;
                }
            }

            if (Check(Tokens.TokenType.Coma))
            {
                Consume(Tokens.TokenType.Coma, "Expected ',' after source list");
                Console.WriteLine("Debug: Consumed ',' after source list");
            }

            return sources;
        }


        private string ParseSingle()
        {
            Consume(Tokens.TokenType.DoblePunto, "Expected ':' after 'Single'");

            var token = Advance();
            // Verificar si el valor de mi Token es el correcto
            if (token.Value != "true" && token.Value != "false")
            {
                throw new Exception($"Invalid value '{token.Value}' for 'Single' at line {token.Row}. Expected 'true' or 'false'");
            }

            var singleValue = token.Value;

            Consume(Tokens.TokenType.Coma, "Expected ',' after 'Single'");

            return singleValue;
        }


        private Expression ParsePredicate()
        {
            // Validar la estructura de mi Predicate (...)
            Consume(Tokens.TokenType.DoblePunto, "Expected '(' at the start of predicate");
            Consume(Tokens.TokenType.ParentesisOpen, "Expected '(' after ':'");

            string identifier = Consume(Tokens.TokenType.Identifier, "Expected identifier after '('").Value;

            Consume(Tokens.TokenType.ParentisisClose, "Expected ')' after identifier");

            Consume(Tokens.TokenType.Arrow, "Expected '=>' after ')'");

            // (...)
           
            string field = Consume(Tokens.TokenType.Identifier, "Expected field name after '->'").Value;

            Consume(Tokens.TokenType.Punto, "Expected '.' after field name");

            string property = Consume(Tokens.TokenType.Identifier, "Expected 'Power' or 'Faction' after '.'").Value;

            // Validar las caracteristas posibles
            if (property != "Power" && property != "Faction")
            {
                throw new Exception($"Invalid property: {property}. Expected 'Power' or 'Faction'.");
            }

            // Consumir mi Operador Booleano
            string booleanOperator = ConsumeBooleanOperator();

            // Analizar por casos mi Expresion derecha
            Expression right;
            if (property == "Power")
            {
                int number = int.Parse(Consume(Tokens.TokenType.Number, "Expected number after boolean operator").Value);
                right = new Expression.LiteralExpression(number);
            }
            else
            {
                string faction = Consume(Tokens.TokenType.String, "Expected string after boolean operator").Value;
                right = new Expression.StringLiteralExpression(faction.Trim('"')); 
            }

            var targetExpression = new Expression.IdentifierExpression(identifier);
            var propertyExpression = new Expression.PropertyAccessExpression(targetExpression, property);

            if (Peek().Type == Tokens.TokenType.Coma)
            {
                Consume(Tokens.TokenType.Coma, "Expected ',' after predicate expression");
            }

            return new Expression.BinaryExpression(propertyExpression, booleanOperator, right);
        }


        // Metodo para tomar el operador booleano establecido en mi Predicate
        private string ConsumeBooleanOperator()
        {
            var validOperators = new Dictionary<string, string>
             {
                { "MenorQ", "<" },
                { "MenorIgualQ", "<=" },
                { "MayorQ", ">" },
                { "MayorIgualQ", ">=" },
                { "Igual", "==" }
             };

            Tokens.TokenType tokenType = Peek().Type;

            string op = Consume(tokenType, "Expected boolean operator").Value;

            if (!validOperators.ContainsKey(tokenType.ToString()))
            {
                throw new Exception($"Invalid boolean operator: {op}. Expected one of 'MenorQ', 'MenorIgualQ', 'MayorQ', 'MayorIgualQ', 'Igual'.");
            }

            return op;
        }


        // Evaluar mi PRedicate
        public bool EvaluatePredicate(Expression.BinaryExpression binary, string cardName, int cardAttack)
        {
            bool isRightOperandString = binary.Right is Expression.StringLiteralExpression;

            // Analizar por casos mi Predicate
            if (isRightOperandString)
            {
                string rightStringValue = (binary.Right as Expression.StringLiteralExpression).Value;

                string leftStringValue = cardName;
                return EvaluateStringComparison(cardName, rightStringValue, binary.Operator);
            }
            else
            {
                return EvaluateIntegerComparison(cardAttack, (binary.Right as Expression.LiteralExpression).Value, binary.Operator);
            }
        }


        // Evaluar si las Facciones coinciden
        private bool EvaluateStringComparison(string leftValue, string rightValue, string booleanOperator)
        {
            switch (booleanOperator)
            {
                case "==":
                    return leftValue == rightValue;
                case "!=":
                    return leftValue != rightValue;
                default:
                    throw new Exception($"Unknown string comparison operator {booleanOperator}");
            }
        }


        // Validar si se cumple eloperador booleano entre los Numeros
        private bool EvaluateIntegerComparison(int leftValue, int rightValue, string booleanOperator)
        {
            switch (booleanOperator)
            {
                case "==":
                    return leftValue == rightValue;
                case "!=":
                    return leftValue != rightValue;
                case "<":
                    return leftValue < rightValue;
                case "<=":
                    return leftValue <= rightValue;
                case ">":
                    return leftValue > rightValue;
                case ">=":
                    return leftValue >= rightValue;
                case "&&":
                    return leftValue != 0 && rightValue != 0; 
                case "||":
                    return leftValue != 0 || rightValue != 0; 
                default:
                    throw new Exception($"Unknown integer comparison operator {booleanOperator}");
            }
        }


        // Parsear PostAction (Igual que mi Action)
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


        // Tomar los Prametros de mi Efecto
        private Dictionary<string, string> TakeParamsOfMyEffect(string nameOfEffect)
        {
            string pathOfTexts = Path.Combine(Application.dataPath, pathTexts);

            Effect effect = new Effect();

            if (Directory.Exists(pathOfTexts))
            {
                // Tomar los textos de los efectos
                string[] txt = Directory.GetFiles(pathTexts, "*.txt");

                foreach (string file in txt)
                {
                    // Si el Nombre de mi Efecto Coinciden
                    if (file == nameOfEffect)
                    {
                        // Proceso de Lexer y Parser
                        string content = File.ReadAllText(file);
                        List<(string, int)> listOfWords = Lexer.GetWordsAndRow(content, Lexer.specialCaracter);
                        List<Tokens> listTokens = Lexer.GetTokens(listOfWords);

                        ParserEffect parser = new ParserEffect(listTokens);
                        effect = parser.ParseEffect();  
                    }
                }
            }

            return effect.Params;
        }
    }
}