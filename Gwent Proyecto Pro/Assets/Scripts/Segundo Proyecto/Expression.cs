using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_Expression
{
    public abstract class Expression
    {

        // Clase Para manejar expresiones binarias (operaciones aritméticas y Booleanas)
        public class BinaryExpression : Expression
        {
            public Expression Left { get; }
            public string Operator { get; }
            public Expression Right { get; }
            public BinaryExpression(Expression left, string op, Expression right) 
            { 
                Left = left;
                Operator = op;
                Right = right;
            }
        }


        // Clase para manejar expresiones literales de enteros
        public class LiteralExpression : Expression 
        { 
            public int Value { get; }
            public LiteralExpression(int value)
            {
                Value = value;
            }
        }


        // Similar al anteior pero literales de cadenas de texto
        public class StringLiteralExpression : Expression
        {
            public StringLiteralExpression(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }


        // Clase para manejar asignaciones (un valor se asigna a una variable)
        public class AssignmentExpression : Expression
        {
            public AssignmentExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }

            public Expression Left { get; }
            public Expression Right { get; }
        }


        // Clase para manejar bucles while
        // Una expression while posee una COndicon y un Cuerpo (Body)
        public class WhileExpression : Expression
        {
            public WhileExpression(Expression condition, Expression body)
            {
                Condition = condition;
                Body = body;
            }

            public Expression Condition { get; }
            public Expression Body { get; }
        }


        // Clase para manejar bloques de código
        public class BlockExpression : Expression
        {
            public BlockExpression(List<Expression> expressions)
            {
                Expressions = expressions;
            }

            public List<Expression> Expressions { get; }
        }


        // Clase para manejar identificadores (nombres de variables)
        public class IdentifierExpression : Expression
        {
            public IdentifierExpression(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }


        // Clase para manejar el acceso a propiedades de 'targets'
        public class TargetPropertyExpression : Expression
        {
            public TargetPropertyExpression(string propertyName, string identifier)
            {
                PropertyName = propertyName;
                Identifier = identifier;
            }

            public string PropertyName { get; }
            public string Identifier { get; }
        }


        // Clase para manejar el acceso a propiedades de 'context'
        public class ContextPropertyExpression : Expression
        {
            public ContextPropertyExpression(string propertyName, string identifier)
            {
                PropertyName = propertyName;
                Identifier = identifier;
            }

            public string PropertyName { get; }
            public string Identifier { get; }
        }


        // Clase para manejar el acceso a propiedades en general 
        // Contiene una expresión de destino (Target) y el nombre de la propiedad (Property).
        public class PropertyAccessExpression : Expression
        {
            public PropertyAccessExpression(Expression target, string property)
            {
                Target = target;
                Property = property;
            }

            public Expression Target { get; }
            public string Property { get; }
        }


        // Clase para manejar llamadas a métodos
        // Contiene una expresión de destino (Target), el nombre del método (Method) y una lista de argumentos (Arguments).
        public class MethodCallExpression : Expression
        {
            public MethodCallExpression(Expression target, string method, List<Expression> arguments)
            {
                Target = target;
                Method = method;
                Arguments = arguments;
            }

            public Expression Target { get; }
            public string Method { get; }
            public List<Expression> Arguments { get; }
        }
    }
}

