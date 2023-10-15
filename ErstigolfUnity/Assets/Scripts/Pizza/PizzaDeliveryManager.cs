using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PizzaDeliveryManager : MonoBehaviour
{
    #region instance
    public static PizzaDeliveryManager instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] PizzaTarget[] pizzaTargets;
    [SerializeField] PizzaType[] pizzaTypes;
    [SerializeField] AudioSource newOrderSound;
    [SerializeField] AudioSource orderSuccessfullSound;
    [SerializeField] AudioSource orderFailedSound;

    [SerializeField] int maxConcurrentDeliveryCount;
    [Tooltip("The time for a new order, when there is no current order")]
    [SerializeField] float emptyOrderTime;
    [Tooltip("The time for getting a new Order, when there already is an active Order")]
    [SerializeField] float orderCooldownTime = 3f;
    float lastCooldownTime = 0;

    // only deliver one pizza type per location to avoid overstressing of pizzamaker
    List<PizzaTarget> availableTargets;

    [Space]
    [Header("Scripted Orders")]
    [SerializeField] private List<PizzaOrderEvent> pizzaOrdersScripted;
    private int doneTo = 0;

    private float StartTime = 0f;
    
    [System.Serializable]
    public class PizzaOrderEvent
    {
        public int AppearenceTime = 0;

        public PizzaTarget orderLocation;
        public PizzaType pizzaType;
        public int Count;
    }

    private System.Random rng;

    private void Start() {
        availableTargets = new List<PizzaTarget>(pizzaTargets);
        score = 0;
        StartTime = Time.time;
        if (PlayerPrefs.HasKey("Seed")){
            int seed = PlayerPrefs.GetInt("Seed");
            if (seed != 0){
                pizzaOrdersScripted = new List<PizzaOrderEvent>();
                rng = new System.Random(seed);
            }
        }
        pizzaOrdersScripted.OrderBy(a => a.AppearenceTime);
    }

    private void Update()
    {
        if(pizzaOrdersScripted.Count > doneTo)
        {
            if(Time.time > pizzaOrdersScripted[doneTo].AppearenceTime + StartTime)
            {
                AddNextScriptedOrder();
            }
        } else
        {
            if (currentOrderCount == 0){
                lastCooldownTime = Mathf.Min(Time.time -orderCooldownTime + emptyOrderTime, lastCooldownTime);
            }
            if(currentOrderCount < maxConcurrentDeliveryCount && lastCooldownTime + orderCooldownTime < Time.time)
            {
                AddRandomOrder();
            }
        }
    }

    int currentOrderCount = 0;
    private void AddRandomOrder()
    {
        int count = rng.Next(2, 6);
        lastCooldownTime = Time.time;
        if(availableTargets.Count > 0)
        {
            int target = rng.Next(0, availableTargets.Count);
            int type = rng.Next(0, pizzaTypes.Length);

            AddOrder(pizzaTypes[type], count, availableTargets[target]);
        }
    }

    private void AddNextScriptedOrder()
    {
        PizzaOrderEvent pizzaOrder = pizzaOrdersScripted[doneTo++];
        if(!availableTargets.Contains(pizzaOrder.orderLocation))
        {
            Debug.LogWarning("Overlapping orders in script!");
            return;
        }
        AddOrder(pizzaOrder.pizzaType, pizzaOrder.Count, pizzaOrder.orderLocation);
    }

    private void AddOrder(PizzaType type, int count, PizzaTarget target){
        lastCooldownTime = Time.time;

        target.RequirePizza(type, count);

        currentOrderCount++;
        availableTargets.Remove(target);

        newOrderSound.Play();
    }

    public void DeliveredPizza(int points)
    {
        IncreaseScore(points);
        orderSuccessfullSound.Play();
    }

    public void DeliveredFinalPizza(PizzaTarget target){
        OnTargetDeactivate(target);
    }

    private void OnTargetDeactivate(PizzaTarget target){
        
        availableTargets.Add(target);
        currentOrderCount--;
    }

    public void TimeOut(PizzaTarget target){
        OnTargetDeactivate(target);
        AlertSystem.Message("Too slow: " + target.Name + " cancelled their order");
        orderFailedSound.Play();
    }

    public int score {get; private set;}
    public void IncreaseScore(int amount)
    {
        score += amount;
        UiManager.instance.UpdateScore(score);
    }

}


