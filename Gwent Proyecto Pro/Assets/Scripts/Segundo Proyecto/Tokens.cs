using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_Token
{
    public class Tokens
    {
        public string Value { get; set; }

        public TokenType Type { get; set; }
        public int Row { get; set; }


        public Tokens(string value, TokenType type, int row)
        {
            Value = value;
            Type = type;
            Row = row;
        }

        public enum TokenType
        {
            // Caracteres
            Number, Identifier, String,

            // Operadores Matemáticos
            Plus, PlusPlus, PlusFunc, Menos, MenosMenos, MenosFunc, Multi, Potencia, Division,

            // Operadores Booleanos
            And, Or, MenorQ, MenorIgualQ, MayorQ, MayorIgualQ, Igual,

            // Asignación
            Asignacion, Arrow,

            // Concatenacion
            Concatenacion, DobleConcatenacion,

            // Ciclos
            For, While,

            // Signos
            Punto, PuntoComa, Coma, DoblePunto, ParentesisOpen, ParentisisClose, LlaveOpen, LlaveClose, Comillas,

            // Palabras Claves Effect
            effect, Name, Params, Action, TriggerPlayer, Board, HandOfPlayer, Hand, FieldOfPlayer, Field,
            GraveyardOfPlayer, Graveyard, DeckOfPlayer, Find, Push, SendBottom, Pop, Remove, Shuffle,

            // Palabras Claves Card
            card, Type, Faction, Power, Range, OnActivation, Effect, Selector, Source, Single, Predicate, PostAction
        }

    }

}
