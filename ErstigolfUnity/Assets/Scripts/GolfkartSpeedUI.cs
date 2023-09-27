using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GolfkartSpeedUI : MonoBehaviour
{
    [SerializeField] private GolfkartSpeed golfkartSpeed;
    [SerializeField] private TextMeshProUGUI SpeedTM;
    private void Update()
    {
        SpeedTM.text = ((int)golfkartSpeed.GetAvgSpeedOverTimeRecord()).ToString() + " m/s";
    }
}
