using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTarget : MonoBehaviour
{
    int _requirePizzaCount = 0;
    int requirePizzaCount {
        get { 
            return _requirePizzaCount;
        } 
        set {
            _requirePizzaCount = value;
            requirePizzaIndicator(_requirePizzaCount > 0);
            UiManager.instance.UpdateOrders(Name, _requirePizzaCount);
        } 
    
    }

    public void RequirePizza()
    {
        requirePizzaCount++;
    }

    private void Start()
    {
        requirePizzaCount = 0;
    }

    public string Name = "LehrstuhlX";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pizza pizza))
        {
            if (requirePizzaCount > 0)
            {
                requirePizzaCount--;
                PizzaDeliveryManager.instance.DeliveredPizza();
            }
            pizza.Delivered();
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
