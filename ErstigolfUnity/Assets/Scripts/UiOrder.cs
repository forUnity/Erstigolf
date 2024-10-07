using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class UiOrder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI pizzaText;
    [SerializeField] Image barImage;
    [SerializeField] Image barBackground;
    [SerializeField] Slider patienceBar;
    [SerializeField] Gradient patienceGradient;
    [SerializeField] Image[] icons;

    private UiOrder copy;
    public void SetCopy(UiOrder _copy)
    {
        copy = _copy;
    }

    public void UpdateInfo(string name, string pizza, int count, Sprite[] ics)
    {
        if(nameText)
        nameText.text = name;
        if(pizzaText)
        pizzaText.text = pizza;
        if(countText)
        countText.text = count + "x";

        for (int i = 0; i < ics.Length; i++){
            icons[i].sprite = ics[i];
            icons[i].enabled = ics[i] != null;
        }
        
        if(copy)
        {
            copy.UpdateInfo(name, pizza, count, ics);
        }
    }

    public void SetTime(float val){
        Color color = patienceGradient.Evaluate(val);
        barImage.color = color;
        barBackground.color = Color.Lerp(color, Color.white, 0.5f);
        patienceBar.value = val;

        if (copy)
            copy.SetTime(val);
    }


    private void OnDestroy()
    {
        if(copy)
        {
            Destroy(copy.gameObject);
        }
    }
}
