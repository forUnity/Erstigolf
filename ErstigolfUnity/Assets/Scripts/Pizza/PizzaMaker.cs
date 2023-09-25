using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private PizzaThrower thrower;

    private bool[] currentIngredients;

    private ButtonsInput inputs;
    private void Awake() {
        currentIngredients = new bool[PizzaType.ingredientCount];
        inputs = new ButtonsInput();
        inputs.Pizza.Ing1.performed += x => ToggleIngredient(0);
        inputs.Pizza.Ing2.performed += x => ToggleIngredient(1);
        inputs.Pizza.Ing3.performed += x => ToggleIngredient(2);
        inputs.Pizza.Ing4.performed += x => ToggleIngredient(3);
        inputs.Pizza.Ing5.performed += x => ToggleIngredient(4);
        inputs.Pizza.Ing6.performed += x => ToggleIngredient(5);
        inputs.Car.Yellow.performed += x => LoadPizza();
    }

    private void OnEnable() {
        inputs.Enable();
    }

    private void OnDisable() {
        inputs.Disable();
    }

    private void ToggleIngredient(int index){
        currentIngredients[index] = !currentIngredients[index];
    }
    
    private void LoadPizza()
    {
        GameObject p = Instantiate(pizzaPrefab, spawn.position, spawn.rotation, spawn);
        thrower.LoadPizza(p);
        p.GetComponent<Pizza>().ingredients = currentIngredients;
        currentIngredients = new bool[PizzaType.ingredientCount];
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
