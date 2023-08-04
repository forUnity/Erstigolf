using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class PizzaType : ScriptableObject
{
    const int maxIngredientCount = 4;
    public PizzaIngredient[] ingredients;

    public bool Match(PizzaIngredient[] other){
        if (other.Length != ingredients.Length) return false;
        for (int i = 0; i < ingredients.Length; i++){
            if (other[i] != ingredients[i]) return false;
        }
        return true;
    }

    public int[] GetIconsIndices(){
        int[] arr = new int[maxIngredientCount];
        for (int i = 0; i < arr.Length; i++){
            if (i < ingredients.Length){
                arr[i] = (int)(ingredients[i]);
            }
            else {
                arr[i] = -1;
            }
        }
        return arr;
    }

    private void OnValidate() {
        if (ingredients.Length > maxIngredientCount){
            ingredients = new PizzaIngredient[]{
                ingredients[0],
                ingredients[1],
                ingredients[2],
                ingredients[3],
            };
        }
    }
}

public enum PizzaIngredient
{
    asparagus,
    bell_pepper,
    broccoli,
    cheese,
    chili_pepper,
    chicken,
    corn,
    ham,
    mushrooms,
    onions,
    pineapple,
    salami,
    salat,
    tomatos,
}
