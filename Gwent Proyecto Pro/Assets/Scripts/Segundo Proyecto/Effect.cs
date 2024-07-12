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

        #region NEW
        public Selector Selector { get; set; }
        public Effect PostAction { get; set; }
        #endregion

        public Effect() 
        { 
            Params = new Dictionary<string,string>();
        }
    }

}
