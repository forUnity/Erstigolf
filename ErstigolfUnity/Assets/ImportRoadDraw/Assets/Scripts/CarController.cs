using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // TODO: This is placeholder code that does not use physics!
        transform.Translate(transform.forward * moveVertical * speed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up, moveHorizontal * rotationSpeed * Time.fixedDeltaTime);
    }
}
