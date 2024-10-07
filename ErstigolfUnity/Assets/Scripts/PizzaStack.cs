using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaStack : MonoBehaviour
{
    private void Update() {
        if (transform.childCount > 0)
            transform.GetChild(0).localScale = Vector3.Lerp(Vector3.one, transform.GetChild(0).localScale, Mathf.Pow(0.5f, Time.deltaTime * 5f));
        for (int i = 1; i < transform.childCount; i++){
            transform.GetChild(i).localScale = Vector3.zero;
        }
    }
}
