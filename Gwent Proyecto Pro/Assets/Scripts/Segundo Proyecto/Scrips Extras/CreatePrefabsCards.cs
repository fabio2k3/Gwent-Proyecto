using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatePrefabsCards : MonoBehaviour
{
    // Ruta Carpeta Textos Cartas
    string textFolder = "Assets/Textos/Texts Cards";
    // Ruta Carpeta Destino Textos Cartas
    string newTextFolder = "Assets/Textos/Texts Cards/Used";
    // Ruta Carpeta Prefabs
    string prefabsFolderOrc = "Assets/Prefabs/2nd Project/For Use/Orc";

    public void CreatePrefabs()
    {
        string[] textCards = Directory.GetFiles(textFolder, "*.txt");
        foreach (string textCard in textCards)
        {
            if(Directory.GetFiles(prefabsFolderOrc).Length > 0)
            {
                string content = File.ReadAllText(textCard);
                Debug.Log(content);
            }
        }
    }
}
