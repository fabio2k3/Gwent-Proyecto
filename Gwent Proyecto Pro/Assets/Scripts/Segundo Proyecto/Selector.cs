using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_Selector
{
    public class Selector
    {
        public List<string> Source { get; set; }
        public bool Single { get; set; }
        public string Predicate { get; set; }

        public Selector()
        {
            Source = new List<string>();
        }
    }
}

