using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderEscala : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 originalScale;
    private bool isMouseOver = false;

    void Start()
    {
        originalScale = transform.localScale;
    }


    void OnMouseEnter()
    {
        if(!isMouseOver)
        {
            transform.localScale = targetScale;
            isMouseOver = true;
        }
    }

    private void OnMouseExit()
    {
        if(isMouseOver)
        {
            transform.localScale = originalScale;
            isMouseOver = false;
        }
    }
}
