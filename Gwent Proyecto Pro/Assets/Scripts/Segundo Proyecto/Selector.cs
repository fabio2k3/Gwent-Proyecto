using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Gwent_Create_Card_Selector
{
    // Estructura de mi SELECTOR
    public class Selector
    {
        public List<string> Source { get; set; }
        public string Single { get; set; }
        public Expression Predicate { get; set; }
    }
}

