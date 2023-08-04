using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTarget : MonoBehaviour
{
    PizzaType _requiredPizza = null;
    PizzaType RequiredPizza {
        get { 
            return _requiredPizza;
        } 
        set {
            _requiredPizza = value;
            requirePizzaIndicator(_requiredPizza != null);
            UiManager.instance.UpdateOrders(this, _requiredPizza);
        } 
    }

    public void RequirePizza(PizzaType type)
    {
        RequiredPizza = type;
        timeRemain = maxPatience;
    }

    private void Start()
    {
        RequiredPizza = null;
    }

    [SerializeField] float maxPatience = 100;
    float timeRemain;
    public float scoreRatio => timeRemain/maxPatience;

    private void Update() {
        if (RequiredPizza){
            timeRemain -= Time.deltaTime;
            if (timeRemain <= 0){
                PizzaDeliveryManager.instance.TimeOut(this);
                RequiredPizza = null;
            }
        }
    }

    public string Name = "LehrstuhlX";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pizza pizza))
        {
            if (RequiredPizza != null)
            {
                bool correctType = RequiredPizza.Match(pizza.ingredients); // TODO : add pizza type and check
                if (correctType){
                    float timeMultiplier = 1 + scoreRatio;
                    int val = (int)(RequiredPizza.Value * timeMultiplier);
                    PizzaDeliveryManager.instance.DeliveredPizza(this, val);
                    RequiredPizza = null;
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
    private void requirePizzaIndicator(bool show)
    {
        tempGfx.SetActive(show);
    }
    #endregion
}
