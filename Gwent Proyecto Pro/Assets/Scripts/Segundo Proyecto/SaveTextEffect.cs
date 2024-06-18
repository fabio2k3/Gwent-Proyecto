using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveTextEffect : MonoBehaviour
{
    public TMP_InputField inputFieldEffect;
    public Button saveButton;
    private string saveKeyPrefix = "TextEffect_";
    private int textCounter;
    void Start()
    {
        textCounter = PlayerPrefs.GetInt("TextCount", 0);

        saveButton.onClick.AddListener(SaveText);
    }

    public void SaveText()
    {
        string textToSave = inputFieldEffect.text;
        string saveKey = saveKeyPrefix + textCounter;
        PlayerPrefs.Save();
        // Debug.Log("Texto guardado con la clave " + saveKey + ": " + textToSave);

        textCounter++;
        PlayerPrefs.SetInt("TextCounter", textCounter);
        PlayerPrefs.Save();
    }

    public List<string> LoadAlltexts()
    {
        List<string> allTexts = new List<string>();

        for(int i = 0; i < textCounter; i++)
        {
            string saveKey = saveKeyPrefix + i;
            if(PlayerPrefs.HasKey(saveKey))
            {
                string loadText = PlayerPrefs.GetString(saveKey);
                allTexts.Add(loadText);
                Debug.Log("Texto cargado con la clave " + saveKey + ": " + loadText);
            }
        }

        return allTexts;
    }
}
