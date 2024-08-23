using Gwent_Create_Card_Selector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_Expression
{
    public class Effect
    {
        public string Name {  get; set; }

        public Dictionary<string,string> Params { get; set; }

        public Expression Action { get; set; } // Propiedad para la acción

        public string Classification { get; set; } // Nueva propiedad para la clasificación

        public Effect() 
        { 
            Params = new Dictionary<string,string>();
        }
    }

}
