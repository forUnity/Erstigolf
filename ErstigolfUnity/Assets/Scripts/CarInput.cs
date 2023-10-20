using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    [HideInInspector] public bool brake;
    public int steer => brake ? 0 : ((left ? -1 : 0) + (right ? 1 : 0));

    private ButtonsInput inputs;
    private bool left;
    private bool right;

    private bool solo;

    private void Awake()
    {
        inputs = new ButtonsInput();
        if (PlayerPrefs.HasKey("SoloMode") && PlayerPrefs.GetInt("SoloMode") == 1){
            solo = true;
            inputs.Solo.Steer.performed += x => { float v = x.ReadValue<float>(); left = v < 0; right = v > 0;};
            inputs.Solo.Brake.performed += x => brake = x.ReadValueAsButton();
        }
        else {
            inputs.Car.Black_Press.performed += x => right = true;
            inputs.Car.Black_Release.performed += x => right = false;
            inputs.Car.Red_Press.performed += x => left = true;
            inputs.Car.Red_Release.performed += x => left = false;
        }
    }

    private void Start() {
        PauseMenu.toggleEvent += x => {if (x) inputs.Disable(); else inputs.Enable();};
    }

    private void Update() {
        if (!solo){
            brake = left && right;
        }
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
