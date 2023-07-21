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

    [SerializeField] private List<PizzaTarget> pizzaTargets;
    [SerializeField] private int maxConcurrentDeliveryCount = 3;

    [SerializeField] private float orderCooldownTime = 3f;
    [SerializeField] private float lastCooldownTime = 0;
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
        int target = Random.Range(0, pizzaTargets.Count);
        pizzaTargets[target].RequirePizza();
    }

    public void DeliveredPizza()
    {
        currentOrderCount--;
        IncreaseScore();
    }

    int score = 0;
    public void IncreaseScore()
    {
        score++;
        UiManager.instance.UpdateScore(score);
    }

}


