using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
public class MenuSettingToggle : MonoBehaviour
{
    public string PlayerPrefName;
    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (PlayerPrefs.HasKey(PlayerPrefName))
            toggle.isOn = PlayerPrefs.GetInt(PlayerPrefName) == 1;
        toggle.onValueChanged.AddListener(OnChange);
    }

    public void OnChange(bool val)
    {
        PlayerPrefs.SetInt(PlayerPrefName, val ? 1 : 0);
    }
}
