using Gwent_Create_Card_ActivatedEffect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gwent_Create_Card_Expression
{
    public class Card
    {
        // Tipo de la Carta
        public string Type { get; set; }

        // Nombre de la Carta
        public string Name { get; set; }

        // Faction de la Carta
        public string Faction { get; set; }

        // Ataque de mi Carta
        public int Power {  get; set; }

        // Lista de las Posiciones que puede Ocupar
        public List<string> Range { get; set; }

        // Lista de los Efectos que se declaran en mi OnActivation
        public List<ActivatedEffect> OnActivation { get; set; } 

        public Card()
        {
            OnActivation = new List<ActivatedEffect>();
        }
    }
}

