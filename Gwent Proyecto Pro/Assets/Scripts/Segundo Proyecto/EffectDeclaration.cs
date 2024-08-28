using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent_Create_Card_ParameterValue;

namespace Gwent_Create_Card_EffectDeclaration
{
    public class EffectDeclaration
    {
        // Nombre del Efecto
        public string Name { get; set; }

        // Parametros declarados (Nombre - (Valor y Tipo))
        public Dictionary<string, ParameterValue> Params { get; set; }

        public EffectDeclaration()
        {
            Params = new Dictionary<string, ParameterValue>();
        }
    }
}
