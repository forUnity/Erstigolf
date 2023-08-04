using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    [SerializeField] private GameObject pizzaPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private PizzaThrower thrower;

    Pizza currentPizza;
    List<PizzaIngredient> currentIngredients = new List<PizzaIngredient>();

    private void Awake()
    {
        AssignInputs();
    }

    KeyCode[] inputKeys;
    private void AssignInputs()
    {
        List<KeyCode> availableKeyCodes = new List<KeyCode>();
        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            availableKeyCodes.Add((KeyCode)i);
        }
        inputKeys = new KeyCode[(int)PizzaIngredient.last];
        for (int i = 0; i < inputKeys.Length; i++)
        {
            int k = Random.Range(0, availableKeyCodes.Count);
            inputKeys[i] = (KeyCode)(availableKeyCodes[k]);
            availableKeyCodes.RemoveAt(k);
        }
    }

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
