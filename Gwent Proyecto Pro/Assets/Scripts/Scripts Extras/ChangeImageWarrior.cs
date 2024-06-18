using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageWarrior : MonoBehaviour
{
    public Sprite imagenPredeterminada;
    public Sprite imagenCondicional;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = imagenPredeterminada; 
    }

    void Update()
    {
        image.sprite = (GameFlow.whoStart == 1 && !gameObject.GetComponent<Cards>().invocated) ? imagenCondicional : imagenPredeterminada;
    }
}
