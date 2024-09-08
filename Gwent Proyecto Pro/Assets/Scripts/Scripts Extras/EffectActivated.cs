using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectActivated : MonoBehaviour
{
    private string gameFlow = "GameFlow";

    private void Start()
    {
        if (gameObject.GetComponent<Cards>().invocated)
        {
            FindGameFlowInAllScenes();
        }
    }

    private void FindGameFlowInAllScenes()
    {
        // Obtener el número de escenas cargadas
        int sceneCount = SceneManager.sceneCount;

        // Iterar por todas las escenas cargadas
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            // Buscar el GameObject en la escena actual
            GameObject myGameObject = GameObject.Find(gameFlow);

            if (myGameObject != null)
            {
                // Buscar el script en el GameObject encontrado
                MonoBehaviour script = myGameObject.GetComponent("EvaluateMyEffects") as MonoBehaviour;
                if (script != null)
                {
                    // Activa el script o llama al método necesario
                    script.enabled = true; // Activar el script

                    // Si necesitas llamar a un método específico en el script
                    // Puedes usar reflexión para invocar métodos específicos si es necesario
                    // Ejemplo: script.Invoke("MethodName", 0f);

                    Debug.Log("Script activado en el GameObject encontrado.");
                }
                
                break; // Opcional: Salir del bucle si encuentras el objeto
            }
        }
    }
}