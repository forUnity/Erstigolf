using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private PizzaThrower thrower;

    [SerializeField] PizzaType[] types;
    // TODO : inputs
    [SerializeField] KeyCode[] inputKeys;

    Pizza currentPizza;
    List<PizzaIngredient> currentIngredients = new List<PizzaIngredient>();

    private void Update()
    {
        if (!thrower.Loaded)
        {
            LoadPizza();
        }
        else {
            PutIngredients();
        }
    }

    private void LoadPizza()
    {
        currentPizza = null;
        currentIngredients = new List<PizzaIngredient>();
        GameObject p = Instantiate(pizzaPrefab, spawn.position, spawn.rotation, spawn);
        thrower.LoadPizza(p);
        currentPizza = p.GetComponent<Pizza>();
    }

    private void PutIngredients()
    {
        for (int i = 0; i < inputKeys.Length; i++){
            if (Input.GetKeyDown(inputKeys[i])) {
                currentIngredients.Add((PizzaIngredient)i);
            }
        }
        UiManager.instance.UpdateLoadedIngredients(currentIngredients.ToArray());
        if (currentPizza != null){
            currentPizza.ingredients = currentIngredients.ToArray();
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
