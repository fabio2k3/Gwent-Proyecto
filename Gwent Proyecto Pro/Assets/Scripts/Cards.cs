using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string name; // Nombre de la carta

    public string type; // Tipo de la carta

    public int attack; // propiedad de ataque
    public int attackAux;

    public bool invocated; // si fue invocada o no

    public int rowInvocated; // fila donde fue invocada
    public int colInvocated; // columna donde fue invocada

    public int climateInvocated; // position del array donde se invocan las carta Climate

    public Vector3 originalPosition; // Posicion Original de la carta dentro del canvas
    public void AssignAttack(string newName, string newType, int newAttack)
    {
        name = newName;
        type = newType;

        if(type == "Unit")
        {
            attack = newAttack;
        }
    }
}

