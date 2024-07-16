using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_EffectDeclaration
{
    public class EffectDeclaration
    {
        public string Name { get; set; }
        public Dictionary<string, ParameterValue> Params { get; set; }

        public EffectDeclaration()
        {
            Params = new Dictionary<string, ParameterValue>();
        }
    }
}
