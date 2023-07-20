using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public void Delivered ()
    {
        Destroy(gameObject);
    }
}
