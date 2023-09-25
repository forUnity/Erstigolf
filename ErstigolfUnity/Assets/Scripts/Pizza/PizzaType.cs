using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class PizzaType : ScriptableObject
{
    public int Value => ingredients.Length * 100;
    public const int ingredientCount = 6;
    public bool[] ingredients;

    public bool Match(bool[] other){
        if (other.Length != ingredients.Length) return false;
        for (int i = 0; i < ingredients.Length; i++){
            if (other[i] != ingredients[i]) return false;
        }
        return true;
    }

    public int[] GetIconsIndices(){
        int[] arr = new int[ingredientCount];
        for (int i = 0; i < arr.Length; i++){
            if (i < ingredients.Length){
                arr[i] = ingredients[i] ? i : -1;
            }
            else {
                arr[i] = -1;
            }
        }
        return arr;
    }

    private void OnValidate() {
        if (ingredients.Length != ingredientCount){
            bool[] newIngredients = new bool[ingredientCount];
            for (int i = 0; i < Mathf.Min(ingredientCount, ingredients.Length); i++){
                newIngredients[i] = ingredients[i];   
            }
            ingredients = newIngredients;
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
    last, // this is for counting and should last, with the first item being 0
}
