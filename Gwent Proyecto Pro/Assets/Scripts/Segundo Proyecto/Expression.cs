using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_Expression
{
    public abstract class Expression
    {
        public class BinaryExpression : Expression
        {
            public Expression Letf { get; }
            public string Operator { get; }
            public Expression Rigth { get; }
            public BinaryExpression(Expression left, string op, Expression right) 
            { 
                Letf = left;
                Operator = op;
                Rigth = right;
            }
        }


        public class LiteralExpression : Expression 
        { 
            public int Value { get; }
            public LiteralExpression(int value)
            {
                Value = value;
            }
        }
    }
}

