using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private PizzaThrower thrower;

    [SerializeField] PizzaType[] types;

    private void Update()
    {
        // TODO : Input
        if (!thrower.Loaded)
        {
            HandlePizzaMake();
        }
    }

    private void HandlePizzaMake()
    {
        int type = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            type = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            type = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            type = 2;
        }
        else
        {
            return;
        }
        Pizza p = Instantiate(pizzaPrefab, spawn.position, spawn.rotation, spawn).GetComponent<Pizza>();
        p.type = types[type];
        thrower.LoadPizza(p.gameObject);
    }

    private void OnDrawGizmos() {
        if (pizzaPrefab && spawn){
            MeshFilter mf = pizzaPrefab.GetComponentInChildren<MeshFilter>();
            if (mf){
                Gizmos.color = Color.red;
                Gizmos.DrawMesh(mf.sharedMesh, spawn.position, spawn.rotation);
            }
        }
    }
}
