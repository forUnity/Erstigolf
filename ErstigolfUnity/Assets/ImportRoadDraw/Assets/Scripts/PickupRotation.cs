using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotation : MonoBehaviour
{
    public float rotationDegreesPerSecond = 50;
    public void Update()
    {
        transform.Rotate(0, rotationDegreesPerSecond * Time.deltaTime, 0);
    }
}