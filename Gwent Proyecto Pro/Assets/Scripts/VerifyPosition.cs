using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyPosition : MonoBehaviour
{
    private bool siendoarrastrado = false;
    private Vector3 position;
    public List<string> validPositions = new List<string>();
    private bool[,] ocupedPosition = new bool[6, 6];

    void OnMouseDown()
    {
        siendoarrastrado = true;
        position = transform.position;
    }

    void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2f));
    }

    void OnTriggerStay(Collider pos)
    {
        Debug.Log("Hola entre");

        foreach(string positions in validPositions)
        {
            if (pos.tag == positions)
            {
                Vector3 coordenas = pos.transform.position;
                transform.position = new Vector3(coordenas.x, coordenas.y, 8.767873f);
                Debug.Log("Hola estoy");
                //pos.tag = "PosicionV";
            }
            else
            {
                transform.position = position;
            }
        }
        
    }

    //void OnTriggerExit2D(Collider2D pos)
    //{
    //    if (pos.CompareTag("PosicionV"))
    //    {
    //        transform.position = posi;
    //        pos.tag = "PosicionI";
    //    }
    //}

    void OnMouseUp()
    {
        siendoarrastrado = false;
    }

}
