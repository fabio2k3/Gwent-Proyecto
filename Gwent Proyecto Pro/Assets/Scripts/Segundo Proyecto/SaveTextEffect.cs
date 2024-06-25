using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SaveTextEffect : MonoBehaviour
{
    public TMP_InputField inputField;
    public static Dictionary<int, string> savedTexts = new Dictionary<int, string>();
    private static int nextKey = 0;

    public void SaveText()
    {
        string textToSave = inputField.text;

        savedTexts.Add(nextKey, textToSave);
        nextKey++;

        //Debug.Log("Texto guardado con clave " + (nextKey - 1) + textToSave);
    }
}
