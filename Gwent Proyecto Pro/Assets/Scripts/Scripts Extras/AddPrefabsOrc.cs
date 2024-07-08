using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AddPrefabsOrc : MonoBehaviour
{
    // Ruta de la Carpeta de los Prefabs
    public string prefabOrcPath = "Assets/Prefabs/2nd Project/Created/Orc";

    public NewBehaviourScript listDeck;

    private void Start()
    {
        // Obtener los prefabs de la carpeta
        string[] prefaFiles = Directory.GetFiles(prefabOrcPath, "*.prefab");

        foreach(string prefab in prefaFiles)
        {
            // Cargar mi prefab como un GameObject
            GameObject myPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefab);

            if(myPrefab != null)
            {
                // Añadir mi Carta creada (Prefab) para la lista de Deck
                listDeck.AddCard(myPrefab);
            }   
        }
    }
}
