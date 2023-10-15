using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPizzaMaker : MonoBehaviour
{
    [SerializeField] PizzaMaker pizzaMaker;
    [SerializeField] float bakeDelay = 1f;
    [SerializeField] Image[] bakeIcons;

    bool[] ingredientBuffer = new bool[6];
    ButtonsInput inputs;

    private float lastBake;

    private void Awake() {
        if (PlayerPrefs.HasKey("VirtualPizzaMaker") && PlayerPrefs.GetInt("VirtualPizzaMaker") != 1)
        {
            enabled = false;
            for (int i = 0; i < bakeIcons.Length; i++)
            {
                bakeIcons[i].enabled = false;
            }
            return;
        }

        inputs = new ButtonsInput();
        inputs.VirtualPizzaMaker.Ing1.performed += x => ingredientBuffer[0] = !ingredientBuffer[0];
        inputs.VirtualPizzaMaker.Ing2.performed += x => ingredientBuffer[1] = !ingredientBuffer[1];
        inputs.VirtualPizzaMaker.Ing3.performed += x => ingredientBuffer[2] = !ingredientBuffer[2];
        inputs.VirtualPizzaMaker.Ing4.performed += x => ingredientBuffer[3] = !ingredientBuffer[3];
        inputs.VirtualPizzaMaker.Ing5.performed += x => ingredientBuffer[4] = !ingredientBuffer[4];
        inputs.VirtualPizzaMaker.Ing6.performed += x => ingredientBuffer[5] = !ingredientBuffer[5];
        inputs.VirtualPizzaMaker.Bake.performed += x => OnEnter();

        lastBake = -bakeDelay;
    }

    private void Start() {
        PauseMenu.toggleEvent += x => {if (x) inputs.Disable(); else inputs.Enable();};
    }

    private void OnEnter(){
        if (lastBake + bakeDelay > Time.time)
            return;
        lastBake = Time.time;
        pizzaMaker.LoadPizza(ingredientBuffer);
        ingredientBuffer = new bool[6];
    }

    private void Update() {
        for (int i = 0; i < ingredientBuffer.Length; i++){
            bakeIcons[i].enabled = ingredientBuffer[i];
        }
    }

    private void OnEnable() => inputs.Enable();
    private void OnDisable() => inputs.Disable();
}
