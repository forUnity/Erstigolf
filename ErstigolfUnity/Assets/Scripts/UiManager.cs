using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    private void Awake()
    {
        OrderersToCount = new Dictionary<PizzaTarget, UiOrder>();
        if (!instance)
            instance = this;
        else 
            Destroy(gameObject);
    }

    [SerializeField] Transform OrderHolderT;
    [SerializeField] GameObject OrderUIPrefab;
    [SerializeField] Sprite[] icons;
    private Dictionary<PizzaTarget, UiOrder> OrderersToCount;

    private void Update() {
        foreach(PizzaTarget t in OrderersToCount.Keys){
            OrderersToCount[t].SetTime(t.scoreRatio);
        }
    }

    public void UpdateOrders(PizzaTarget orderer, PizzaType type)
    {
        if (OrderersToCount.ContainsKey(orderer))
        {
            if (type == null)
            {
                Destroy(OrderersToCount[orderer].gameObject);
                OrderersToCount.Remove(orderer);
            } 
            else
            {
                OrderersToCount[orderer].UpdateInfo(orderer.Name, type.name, GetIcons(type));
            }
        } 
        else
        {
            if (type == null) return;
            UiOrder uiElement = Instantiate(OrderUIPrefab, OrderHolderT).GetComponent<UiOrder>();
            uiElement.gameObject.SetActive(true);
            OrderersToCount.Add(orderer, uiElement);
            UpdateOrders(orderer, type);
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

    [SerializeField] GameObject icon;
    [SerializeField] Transform iconHolder;
    List<Image> currentIcons = new List<Image>();
    public void UpdateLoadedIngredients(PizzaIngredient[] ingredients){
        while (currentIcons.Count < ingredients.Length){
            currentIcons.Add(Instantiate(icon, iconHolder).GetComponent<Image>());
        }
        for (int i = 0; i < currentIcons.Count; i++){
            bool visible = i < ingredients.Length;
            currentIcons[i].enabled = visible;
            if (visible){
                currentIcons[i].sprite = icons[(int)ingredients[i]];
            }
        }
    }
 }
