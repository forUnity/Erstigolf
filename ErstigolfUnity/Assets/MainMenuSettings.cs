using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuSettings : MonoBehaviour
{
    [SerializeField] private TMP_InputField comPortField;

    private void Start()
    {
        if(PlayerPrefs.HasKey("SerialPort"))
        {
            comPortField.text = PlayerPrefs.GetString("SerialPort");
        } else
        {
            comPortField.text = "COM3";
        }

        comPortField.onEndEdit.AddListener(s => PlayerPrefs.SetString("SerialPort", s));
    }


}

