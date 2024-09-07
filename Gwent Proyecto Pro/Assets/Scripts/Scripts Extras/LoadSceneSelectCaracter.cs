using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSelectCaracter : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("SelectCharacter");
    }
}
