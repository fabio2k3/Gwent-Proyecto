using Gwent_Create_Card_Expression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gwent_Create_Card_ParserCard;

public class CardObject : MonoBehaviour
{
    // Crear una Instancia de la carta que hare Proximamente
    private Card Card;

    // Propiedades que llevan las Cartas
    public string type;
    public string name;
    public string faction;
    public int power;
    public List<string> range;

    // Propiedades Extras
    public ListImages listImages;
    public UnityEngine.UI.Image Image;
    private GameObject sprite;
    public SpriteRenderer spriteRenderer;

    public void InstanciateMyCard()
    {
        //ParserCard parserCard;
    }
}
