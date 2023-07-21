using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private PizzaThrower thrower;

    private void Update() 
    {
        // TODO : Input
        if (!thrower.Loaded && Input.GetKeyDown(KeyCode.T)){
            thrower.LoadPizza(Instantiate(pizzaPrefab, spawn.position, spawn.rotation, spawn));
        }
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
