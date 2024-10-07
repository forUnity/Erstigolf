using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuSettings : MonoBehaviour {
    [SerializeField] private string Key = "SerialPort";
    [SerializeField] private string Default = "COM3";
    public enum PrefType
    {
        String,
        Integer
    }
    [SerializeField] private PrefType prefType;
    [SerializeField] private TMP_InputField textField;

    private void Start()
    {
        if(PlayerPrefs.HasKey(Key))
        {
            textField.text = prefType == PrefType.String ? PlayerPrefs.GetString(Key) : PlayerPrefs.GetInt(Key).ToString();
        } else
        {
            textField.text = Default;
        }

        textField.onEndEdit.AddListener(OnChange);
    }

    private void OnChange(string value){
         if (prefType == PrefType.String) { 
            PlayerPrefs.SetString(Key, value); 
        } else { 
            PlayerPrefs.SetInt(Key, int.Parse(value)); 
        }
    }
}

