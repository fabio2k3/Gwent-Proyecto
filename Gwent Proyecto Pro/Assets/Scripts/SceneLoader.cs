using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool collisionOcurred = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collisionOcurred && collision.gameObject.CompareTag("TablePlayer1"))
            collisionOcurred = true;
    }

    private void Update()
    {
        if (collisionOcurred && Input.GetKeyUp(KeyCode.F))
            SceneManager.LoadScene("GameCarts");
    }
}
