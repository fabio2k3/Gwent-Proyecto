using Gwent_Create_Card_EffectDeclaration;
using Gwent_Create_Card_Selector;
using Gwent_Create_Card_PostAction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_ActivatedEffect
{
    public class ActivatedEffect 
    {
        public EffectDeclaration Effect { get; set; }
        public Selector Selector { get; set; }
        public PostAction PostAction { get; set; }
    }
}
