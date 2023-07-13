using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10;
    public float mapRadius = 450;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.insideUnitSphere;
        direction.y = 0;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > mapRadius && Vector3.Dot(transform.position, direction) > 0)
        {
            while (Vector3.Dot(transform.position, direction) > 0)
            {
                direction = Random.insideUnitSphere;
                direction.y = 0;
                direction.Normalize();
            }
        }
        transform.position += direction * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tornado hit something");
        if (other.gameObject.tag == "Player")
        {
            other.attachedRigidbody.velocity = new Vector3(1,2,1).normalized * 100;
            GameManager.instance.Loose();
        }
    }
}
