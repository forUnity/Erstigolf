using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public PizzaIngredient[] ingredients;
    [SerializeField] private GameObject deliverParticles;
    public void Delivered ()
    {
        Instantiate(deliverParticles, transform.position, transform.rotation);
        AlertSystem.Message("Getroffen!");
        Destroy(gameObject);
    }

    public void UnwantedDelivery(){
        AlertSystem.Message("Hier wurde nichts bestellt");
    }

    public void WrongDelivery(){
        AlertSystem.Message("Das wurde nicht bestellt");
    }
}
