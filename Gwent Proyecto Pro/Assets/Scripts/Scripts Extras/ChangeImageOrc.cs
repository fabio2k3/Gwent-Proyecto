using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageOrc : MonoBehaviour
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
        image.sprite = (GameFlow.whoStart == 0 && !gameObject.GetComponent<Cards>().invocated) ? imagenCondicional : imagenPredeterminada;
    }
}
