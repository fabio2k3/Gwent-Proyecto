using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_Expression
{
    public abstract class Expression
    {
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

