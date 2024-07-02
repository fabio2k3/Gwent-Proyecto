using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_Expression
{
    public class Effect
    {
        public string Name {  get; set; }
        public Dictionary<string,string> Params { get; set; }

        public Effect() 
        { 
            Params = new Dictionary<string,string>();
        }
    }

}
