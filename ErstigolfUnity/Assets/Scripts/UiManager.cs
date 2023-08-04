using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    private void Awake()
    {
        OrderersToCount = new Dictionary<string, UiOrder>();
        if (!instance)
            instance = this;
        else 
            Destroy(gameObject);
    }

    [SerializeField] Transform OrderHolderT;
    [SerializeField] GameObject OrderUIPrefab;
    [SerializeField] Sprite[] icons;
    private Dictionary<string, UiOrder> OrderersToCount;

    public void UpdateOrders(string ordererName, PizzaType type)
    {
        if (OrderersToCount.ContainsKey(ordererName))
        {
            if (type == null)
            {
                Destroy(OrderersToCount[ordererName].gameObject);
                OrderersToCount.Remove(ordererName);
            } 
            else
            {
                OrderersToCount[ordererName].UpdateInfo(ordererName, type.name, GetIcons(type));
            }
        } 
        else
        {
            if (type == null) return;
            UiOrder uiElement = Instantiate(OrderUIPrefab, OrderHolderT).GetComponent<UiOrder>();
            uiElement.gameObject.SetActive(true);
            OrderersToCount.Add(ordererName, uiElement);
            UpdateOrders(ordererName, type);
        }
    }

    private Sprite[] GetIcons(PizzaType type){
        int[] inds = type.GetIconsIndices();
        int count = inds.Length;
        Sprite[] arr = new Sprite[count];
        for (int i = 0; i < count; i++){
            if (inds[i] < 0) {
                arr[i] = null;
            }
            else {
                arr[i] = icons[inds[i]];
            }
        }
        return arr;
    }

    [SerializeField] private TextMeshProUGUI scoreTm;
    public void UpdateScore(int score)
    {
        scoreTm.text = score.ToString();
    }

 }
