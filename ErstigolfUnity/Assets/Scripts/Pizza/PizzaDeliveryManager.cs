using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] AudioSource sound;
    [SerializeField] int maxConcurrentDeliveryCount;
    [Tooltip("The time for a new order, when there is no current order")]
    [SerializeField] float emptyOrderTime;
    [Tooltip("The time for getting a new Order, when there already is an active Order")]
    [SerializeField] float orderCooldownTime = 3f;
    float lastCooldownTime = 0;

    // only deliver one pizza per location to avoid overstressing of pizzamaker
    List<PizzaTarget> availableTargets;

    private void Start() {
        availableTargets = new List<PizzaTarget>(pizzaTargets);
        score = 0;
    }

    private void Update()
    {
        if (currentOrderCount == 0){
            lastCooldownTime = Mathf.Min(Time.time -orderCooldownTime + emptyOrderTime, lastCooldownTime);
        }
        if(currentOrderCount < maxConcurrentDeliveryCount && lastCooldownTime + orderCooldownTime < Time.time)
        {
            AddPizza(Random.Range(2, 6));
        }
    }

    int currentOrderCount = 0;
    private void AddPizza(int count)
    {
        lastCooldownTime = Time.time;
        currentOrderCount++;
        int target = Random.Range(0, availableTargets.Count);
        int type = Random.Range(0, pizzaTypes.Length);
        availableTargets[target].RequirePizza(pizzaTypes[type], count);
        availableTargets.RemoveAt(target);
        sound.Play();
    }

    public void DeliveredPizza(int points)
    {
        IncreaseScore(points);
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
        AlertSystem.Message("Zu langsam für " + target.Name);
    }

    public int score {get; private set;}
    public void IncreaseScore(int amount)
    {
        score += amount;
        UiManager.instance.UpdateScore(score);
    }

}


