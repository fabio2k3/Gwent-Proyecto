using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_ParameterValue
{
    public class ParameterValue
    {
        // Tipo de Parametro
        public ParameterType Type { get; set; } 

        // Valor de dicho Parametro (Object xq puede ser => Number, String
        public object Value { get; set; }

        public ParameterValue(ParameterType type, object value)
        {
            Type = type;
            Value = value;
        }
    }

    // Todos los posibles tipos de Parametros
    public enum ParameterType
    {
        String,
        Number,
        Boolean
    }
}

