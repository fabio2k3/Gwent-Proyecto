using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Controlar la velocidad de nuestro personaje
    public float runSpeed = 7;

    // Controlar la velocidad al girar
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y; // varaibles para saber en que eje nos estamos moviendo

    void Update()
    {
        x = Input.GetAxis("Horizontal"); // saber si nos movemos a la izquierda o la derecha

        y = Input.GetAxis("Vertical"); // saber si nos movemos hacia delante o hacia atrás

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);

        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);


        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);
    }
}
