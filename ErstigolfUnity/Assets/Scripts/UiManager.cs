using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        OrderersToCount = new Dictionary<string, UiOrder>();
    }


    [SerializeField] private Transform OrderHolderT;
    [SerializeField] private GameObject OrderUIPrefab;
    private Dictionary<string,UiOrder> OrderersToCount;
    public void UpdateOrders(string ordererName, int newCount)
    {
        if(OrderersToCount.ContainsKey(ordererName))
        {
            if(newCount <= 0)
            {
                Destroy(OrderersToCount[ordererName].gameObject);
            } else
            {
                OrderersToCount[ordererName].UpdateInfo(ordererName, newCount);
            }
        } else
        {
            if (newCount <= 0) return;
            UiOrder uiElement = Instantiate(OrderUIPrefab, OrderHolderT).GetComponent<UiOrder>();
            uiElement.gameObject.SetActive(true);
            OrderersToCount.Add(ordererName, uiElement);
            UpdateOrders(ordererName, newCount);
        }
    }

    [SerializeField] private TextMeshProUGUI scoreTm;
    public void UpdateScore(int score)
    {
        scoreTm.text = score.ToString();
    }

 }
