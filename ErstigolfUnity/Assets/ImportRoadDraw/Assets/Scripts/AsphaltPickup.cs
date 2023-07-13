using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsphaltPickup : MonoBehaviour
{
    public float distanceAdded = 50; // in meters

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StreetDrawer.Instance.AddAsphalt(distanceAdded);
            Destroy(gameObject);
        }
    }
}
