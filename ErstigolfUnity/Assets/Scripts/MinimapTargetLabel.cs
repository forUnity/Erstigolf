using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTargetLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string targetName = GetComponentInParent<PizzaTarget>().Name;

        GetComponent<TMPro.TextMeshPro>().text = targetName;
    }
}
