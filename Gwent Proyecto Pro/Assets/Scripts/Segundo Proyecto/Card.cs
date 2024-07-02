using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_Expression
{
    public class Card
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public int Power {  get; set; }
        public List<string> Range { get; set; }
    }
}

