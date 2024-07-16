using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_ParameterValue
{
    public class ParameterValue
    {
        public ParameterType Type { get; set; }
        public object Value { get; set; }

        public ParameterValue(ParameterType type, object value)
        {
            Type = type;
            Value = value;
        }
    }


    public enum ParameterType
    {
        String,
        Number,
        Boolean
    }
}

