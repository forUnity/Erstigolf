using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        gameTimeRemain = gameDuration;
    }

    [SerializeField] List<Image> clockFills;
    [SerializeField] Transform OrderHolderT;
    [SerializeField] Transform OrderHolderTBack;
    [SerializeField] private bool SplitOrderInfo = true;
    [SerializeField] GameObject OrderUIPrefab;
    [SerializeField] GameObject OrderUIPrefabFront;
    [SerializeField] GameObject OrderUIPrefabBack;
    [SerializeField] Sprite[] icons;
    private Dictionary<PizzaTarget, UiOrder> OrderersToCount;


    private void Start()
    {
        OrderHolderTBack.gameObject.SetActive(SplitOrderInfo);
    }

    private void Update() {
        foreach(PizzaTarget t in OrderersToCount.Keys){
            OrderersToCount[t].SetTime(t.scoreRatio);
   
        }
        HandleTime();
    }

    public void UpdateOrders(PizzaTarget orderer, PizzaType type, int count)
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
                OrderersToCount[orderer].UpdateInfo(orderer.Name, type.name, count, GetIcons(type));
            }
        } 
        else
        {
            if (type == null) return;
            UiOrder uiElement = Instantiate(SplitOrderInfo ? OrderUIPrefabFront : OrderUIPrefab, OrderHolderT).GetComponent<UiOrder>();
            uiElement.gameObject.SetActive(true);

            UiOrder uiElementBack = Instantiate(OrderUIPrefabBack, OrderHolderTBack).GetComponent<UiOrder>();
            uiElementBack.gameObject.SetActive(true);
            uiElement.SetCopy(uiElementBack);

            OrderersToCount.Add(orderer, uiElement);
            UpdateOrders(orderer, type, count);
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

    [SerializeField] float gameDuration = 600;
    [SerializeField] TextMeshProUGUI endCountdownText;
    [SerializeField] TextMeshProUGUI endCountdownTextGunner;

    [SerializeField] AudioSource endGameSound;
    [SerializeField] private float showEndCountdownTime = 30f;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI timeText2;
    float gameTimeRemain;

    private void HandleTime(){
        gameTimeRemain -= Time.deltaTime;
        if (gameTimeRemain <= 0){
            SceneManager.LoadSceneAsync(2);
        }
        if(gameTimeRemain <= showEndCountdownTime)
        {
            endCountdownText.text = ((int)gameTimeRemain).ToString();
            endCountdownTextGunner.text = endCountdownText.text;
        } else
        {
            endCountdownText.text = "";
            endCountdownTextGunner.text = endCountdownText.text;
        }
        if(gameTimeRemain <= endGameSound.clip.length && !endGameSound.isPlaying)
        {
            endGameSound.Play();
        }

        timeText.text = (Mathf.CeilToInt(gameTimeRemain)).ToString() + "s";
        timeText2.text = (Mathf.CeilToInt(gameTimeRemain)).ToString() + "s";

        foreach (var item in clockFills)
        {
            item.fillAmount = gameTimeRemain / gameDuration;
        }
    }

    [SerializeField] private TextMeshProUGUI scoreTm;
    [SerializeField] private TextMeshProUGUI scoreTm2;
    public void UpdateScore(int score)
    {
        scoreTm.text = score.ToString() + "Hc";
        scoreTm2.text = score.ToString() + "Hc";
    }

    [SerializeField] GameObject panelPrefab;
    [SerializeField] Transform panelHolder;
    List<PizzaPanel> currentPanels = new List<PizzaPanel>();

    public void UpdateLoadedPizza(Transform[] pizzas){
        while (currentPanels.Count < pizzas.Length){
            currentPanels.Add(Instantiate(panelPrefab, panelHolder).GetComponent<PizzaPanel>());
        }
        for (int i = 0; i < currentPanels.Count; i++){
            bool visible = i < pizzas.Length;
            currentPanels[i].gameObject.SetActive(visible);
            if (visible){
                if (pizzas[i].TryGetComponent(out Pizza pizza)){
                    for (int s = 0; s < Mathf.Min(currentPanels[i].slots.Length, pizza.ingredients.Length); s++){
                        if (pizza.ingredients[s]){
                            currentPanels[i].slots[s].sprite = UiManager.instance.icons[s];
                        }
                        currentPanels[i].slots[s].enabled = pizza.ingredients[s];
                    }
                }
            }
        }
    }
 }
