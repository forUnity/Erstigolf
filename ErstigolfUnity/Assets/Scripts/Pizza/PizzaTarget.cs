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
            UiManager.instance.UpdateOrders(Name, _requiredPizza);
        } 
    }

    public void RequirePizza(PizzaType type)
    {
        RequiredPizza = type;
    }

    private void Start()
    {
        RequiredPizza = null;
    }

    public string Name = "LehrstuhlX";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pizza pizza))
        {
            if (RequiredPizza != null)
            {
                bool correctType = RequiredPizza.Match(pizza.type); // TODO : add pizza type and check
                if (correctType){
                    RequiredPizza = null;
                    PizzaDeliveryManager.instance.DeliveredPizza(this);
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
