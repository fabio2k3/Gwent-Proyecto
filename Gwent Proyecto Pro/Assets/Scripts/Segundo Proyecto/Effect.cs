using Gwent_Create_Card_Selector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gwent_Create_Card_Expression
{
    // Definicion de Efecto en mi Juego (Propiedades)
    public class Effect
    {
        // Nombre de Mi Efecto
        public string Name {  get; set; }

        // Parametros de mi Efecto (Nombre - Tipo)
        public Dictionary<string,string> Params { get; set; }

        // Propiedad Action 
        public Expression Action { get; set; } 

        // Clasificacion de mi Carta
        public string Classification { get; set; } 

        public Effect() 
        { 
            Params = new Dictionary<string,string>();
        }
    }

}
