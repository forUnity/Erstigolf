using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTarget : MonoBehaviour
{
    private int reqCount;
    PizzaType requiredPizza = null;

    public void RequirePizza(PizzaType type, int count)
    {
        requiredPizza = type;
        timeRemain = maxPatience;
        reqCount = count;
    }

    [SerializeField] float maxPatience = 100;
    float timeRemain;
    public float scoreRatio => timeRemain/maxPatience;

    private void Update() {
        if (requiredPizza){
            timeRemain -= Time.deltaTime;
            if (timeRemain <= 0){
                PizzaDeliveryManager.instance.TimeOut(this);
                requiredPizza = null;
            }
        }
        RequirePizzaIndicator(requiredPizza != null);
        UiManager.instance.UpdateOrders(this, requiredPizza, reqCount);
    }

    public string Name = "LehrstuhlX";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pizza pizza))
        {
            if (requiredPizza != null)
            {
                bool correctType = requiredPizza.Match(pizza.ingredients);
                if (correctType){
                    float timeMultiplier = 1 + scoreRatio;
                    int val = (int)(requiredPizza.Value * timeMultiplier);
                    reqCount--;
                    PizzaDeliveryManager.instance.DeliveredPizza(val);
                    if (reqCount <= 0)
                    {
                        requiredPizza = null;
                        PizzaDeliveryManager.instance.DeliveredFinalPizza(this);
                    }
                    pizza.Delivered();
                }
                else {
                    pizza.WrongDelivery();
                }
            }
            else {
                pizza.UnwantedDelivery();
            }
        }
    }

    #region Gfx
    [SerializeField] private GameObject tempGfx;
    private void RequirePizzaIndicator(bool show)
    {
        tempGfx.SetActive(show);
    }
    #endregion
}
