using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SaveTextEffect : MonoBehaviour
{
    public TMP_InputField inputField;
    public string filePath = "Assets/SavedTexts.txt";

    public void SaveText()
    {
        string textToSave = inputField.text;
        string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string textWithTimestamp = $"{timeStamp}: {textToSave}\n";

        File.AppendAllText(filePath, textWithTimestamp);
        Debug.Log("Texto guardado en " + filePath);
    }
}
