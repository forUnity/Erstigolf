using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    private ButtonsInput inputs;
    [HideInInspector] public bool left;
    [HideInInspector] public bool right;
    public int steer => (left ? -1 : 0) + (right ? 1 : 0);

    private void Awake()
    {
        inputs = new ButtonsInput();
        if (PlayerPrefs.HasKey("SoloMode") && PlayerPrefs.GetInt("SoloMode") == 1){
            inputs.Solo.Steer.performed += x => { float v = x.ReadValue<float>(); left = v < 0; right = v > 0;};
            inputs.Solo.Brake.performed += x => { if (x.ReadValueAsButton()){ left = true; right = true;}};
        }
        else {
            inputs.Car.Black_Press.performed += x => right = true;
            inputs.Car.Black_Release.performed += x => right = false;
            inputs.Car.Red_Press.performed += x => left = true;
            inputs.Car.Red_Release.performed += x => left = false;
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
