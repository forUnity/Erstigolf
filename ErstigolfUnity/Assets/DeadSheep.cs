using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSheep : MonoBehaviour
{
    public Transform playerTarget;
    public float speed = 0.1f;
    void Update() {
        transform.position += (playerTarget.position - transform.position).normalized * speed * Time.deltaTime;
        transform.LookAt(playerTarget);
    }

    void OnCollisionEnter(Collision collision)
    {
        //check if the object we collided with has a Pizza GetComponent
        Pizza pizza = collision.gameObject.GetComponent<Pizza>();
        if (pizza != null)
        {
            Destroy(gameObject);
        }
    }
}