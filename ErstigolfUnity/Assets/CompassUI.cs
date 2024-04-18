using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CompassUI : MonoBehaviour
{
    [SerializeField] PizzaDeliveryManager deliveryManager;
    [SerializeField] Transform playerT;
    [SerializeField] private Transform compassT;

    [SerializeField] private TextMeshProUGUI nameText;
    public PizzaTarget FindClosestDeliverySpot()
    {
        PizzaTarget closest = null;
        float dist = float.PositiveInfinity;
        foreach(var targets in deliveryManager.pizzaTargets)
        {
            Vector3 pos = targets.transform.position;
            float sqrdDist = (pos - playerT.position).sqrMagnitude;
            if(sqrdDist < dist)
            {
                closest = targets;
                dist = sqrdDist;
            }
        }
        return closest;
    }

    [SerializeField] private float updateClosestooldown = 1f;
    PizzaTarget closestTarget;
    float lastTime;
    public void Update()
    {
        if(closestTarget == null || updateClosestooldown + lastTime < Time.time)
        {
            lastTime = Time.time;
            closestTarget = FindClosestDeliverySpot();
        }

        nameText.text = closestTarget.Name;

        Vector3 dir = closestTarget.transform.position - playerT.position;
        compassT.position = playerT.position;
        compassT.forward = new Vector3(dir.x, 0f, dir.z); 
    }
}
