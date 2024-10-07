using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private PizzaThrower thrower;

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadPizza(new bool[PizzaType.ingredientCount]);
        }        
    }
#endif
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        if (int.TryParse(msg, out int bits)){
            bool[] ingredients = new bool[PizzaType.ingredientCount];
            for (int i = 0; i < PizzaType.ingredientCount; i++){
                ingredients[i] = bits % 2 == 1;
                bits /= 2;
            }
            LoadPizza(ingredients);
        }
        else { 
            Debug.Log("Message arrived: " + msg);
        }
    }

    
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    public void LoadPizza(bool[] ingredients)
    {
        GameObject p = Instantiate(pizzaPrefab, spawn.position, spawn.rotation, spawn);
        p.transform.localScale = Vector3.zero;
        thrower.AddPizzaAmmo(p);
        p.GetComponent<Pizza>().ingredients = ingredients;
        p.GetComponent<Pizza>().flyParticles.SetActive(false);
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
