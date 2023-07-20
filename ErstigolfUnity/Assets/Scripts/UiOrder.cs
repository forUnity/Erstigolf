using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class UiOrder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    public void UpdateInfo(string name, int count)
    {
        nameText.text = name;
        countText.text = count.ToString();
    }
}
