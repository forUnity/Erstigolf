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
    [SerializeField] int maxConcurrentDeliveryCount = 3;

    [SerializeField] float orderCooldownTime = 3f;
    float lastCooldownTime = 0;

    // only deliver one pizza per location to avoid overstressing of pizzamaker
    List<PizzaTarget> availableTargets;

    private void Start() {
        availableTargets = new List<PizzaTarget>(pizzaTargets);
    }

    private void Update()
    {
        if(currentOrderCount < maxConcurrentDeliveryCount && lastCooldownTime + orderCooldownTime < Time.time)
        {
            AddPizza();
        }
    }

    int currentOrderCount = 0;
    private void AddPizza()
    {
        lastCooldownTime = Time.time;
        currentOrderCount++;
        int target = Random.Range(0, availableTargets.Count);
        int type = Random.Range(0, pizzaTypes.Length);
        availableTargets[target].RequirePizza(pizzaTypes[type]);
        availableTargets.RemoveAt(target);
    }

    public void DeliveredPizza(PizzaTarget target)
    {
        availableTargets.Add(target);
        currentOrderCount--;
        lastCooldownTime = Time.time;
        IncreaseScore();
    }

    int score = 0;
    public void IncreaseScore()
    {
        score++;
        UiManager.instance.UpdateScore(score);
    }

}


