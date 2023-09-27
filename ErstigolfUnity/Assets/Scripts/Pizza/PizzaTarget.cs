using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTarget : MonoBehaviour
{
    private int _reqCount; 
    private int reqCount
    {
        get => _reqCount;
        set
        {
            _reqCount = value;
            updateGraphics(reqCount);
        }
    }
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
                reqCount = 0;
            }
        }
        //RequirePizzaIndicator(requiredPizza != null);
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
    [SerializeField] private GameObject waitingPersonPrefab;
    [SerializeField] private float waitingPeopleRadius;
    private List<GameObject> currentWaitingPeopleGfxs = new List<GameObject>();
    private void updateGraphics(int peopleWaitingCount)
    {
        requirePizzaIndicator(peopleWaitingCount > 0);

        if(currentWaitingPeopleGfxs.Count > peopleWaitingCount)
        {       
            for (int i = 0; i < currentWaitingPeopleGfxs.Count- peopleWaitingCount; i++)
            {
                Destroy(currentWaitingPeopleGfxs[0]);
                currentWaitingPeopleGfxs.RemoveAt(0);
            }

        } else if(currentWaitingPeopleGfxs.Count < peopleWaitingCount)
        {
            for (int i = 0; i < peopleWaitingCount - currentWaitingPeopleGfxs.Count; i++)
            {
                addWaitingPeopleGfx();
            }
        }
    }
    private void addWaitingPeopleGfx()
    {
        if (waitingPersonPrefab == null) return;

        var person = Instantiate(waitingPersonPrefab, transform);
        Vector2 rdm = Random.insideUnitCircle * waitingPeopleRadius;
        person.transform.localPosition = new Vector3(rdm.x, 0f, rdm.y);
        person.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        currentWaitingPeopleGfxs.Add(person);
    }
    [SerializeField] private GameObject requirePizzaIndicatorGfx;//TODO: only visible on Map View
    private void requirePizzaIndicator(bool show)
    {
        requirePizzaIndicatorGfx.SetActive(show);
    }
    #endregion
}
