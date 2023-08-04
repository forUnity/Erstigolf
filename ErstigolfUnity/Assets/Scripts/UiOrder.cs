using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class UiOrder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI pizzaText;
    [SerializeField] Image[] icons;
    public void UpdateInfo(string name, string pizza, Sprite[] ics)
    {
        nameText.text = name;
        pizzaText.text = pizza;
        for (int i = 0; i < ics.Length; i++){
            icons[i].sprite = ics[i];
            icons[i].enabled = ics[i] != null;
        }
    }
}
