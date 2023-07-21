using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    [SerializeField] private GameObject deliverParticles;
    public void Delivered ()
    {
        Instantiate(deliverParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
