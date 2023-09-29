using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTarget : MonoBehaviour
{
    public string Name = "LehrstuhlX";
    [SerializeField] private float timeToCompleteORderInS = 100;
    [Tooltip("Can be Applied with ContextMenu. Height needs to be changed in Zone Shader, Gfx and Collider")]
    [SerializeField] private float targetRadius = 20f;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private GameObject requirePizzaIndicatorGfx;//TODO: only visible on Map View?

    [SerializeField] private GameObject waitingPersonPrefab;
    private List<GameObject> currentWaitingPeopleGfxs = new List<GameObject>();

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
        timeRemain = timeToCompleteORderInS;
        reqCount = count;
    }

    float timeRemain;
    public float scoreRatio => timeRemain/timeToCompleteORderInS;

    private void Start()
    {
        ApplyRadius();
        reqCount = 0;
    }
    [ContextMenu("Apply Radius")]
    public void ApplyRadius()
    {
        if(capsuleCollider)
        {
            capsuleCollider.radius = targetRadius;
        }
        requirePizzaIndicatorGfx.transform.localScale = new Vector3(targetRadius, capsuleCollider ? capsuleCollider.height : targetRadius, targetRadius);
    }
    [ContextMenu("Apply Name")]
    public void ApplyNameEditor()
    {
        gameObject.name = Name;
    }

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
        Vector2 rdm = Random.insideUnitCircle * targetRadius;
        person.transform.localPosition = new Vector3(rdm.x, 0f, rdm.y);
        person.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        currentWaitingPeopleGfxs.Add(person);
    }
    private void requirePizzaIndicator(bool show)
    {
        if(requirePizzaIndicatorGfx)
        requirePizzaIndicatorGfx.SetActive(show);
    }
    #endregion
}
