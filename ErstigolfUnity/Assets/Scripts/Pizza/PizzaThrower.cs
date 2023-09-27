using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaThrower : MonoBehaviour
{
    [SerializeField] private Camera turretCam;
    [SerializeField] private float wideFOV;
    [SerializeField] private float narrowFOV;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private Vector2 wideSensitivity;
    [SerializeField] private Vector2 narrowSensitivity;
    [SerializeField] private float shootSpeed = 200;

    [SerializeField] private Transform rotor;
    [SerializeField] private Transform shootTransform;

    private Vector2 currentLook;

    private ButtonsInput inputs;
    private Vector2 aim;
    private bool narrowAim;
    private void Awake() {
        inputs = new ButtonsInput();
        inputs.Car.Green.performed += x => OnEnter();
        inputs.Car.Aim.performed += x => aim = x.ReadValue<Vector2>();
        inputs.Car.Yellow.performed += x => narrowAim = true;
        inputs.Car.Yellow_Release.performed += x => narrowAim = false;
    }

    private void OnEnable() {
        inputs.Enable();
    }

    private void OnDisable() {
        inputs.Disable();
    }

    private void Update()
    {
        SetArrow();
        UiManager.instance.UpdateLoadedPizza(pizzas.ToArray());
    }

    private void SetArrow()
    {
        Vector2 sensitivity = wideSensitivity;
        float fov = wideFOV;
        if (narrowAim){
            sensitivity = narrowSensitivity;
            fov = narrowFOV;
        }
        currentLook += aim * sensitivity * Time.deltaTime;
        if (turretCam)
            turretCam.fieldOfView = Mathf.Lerp(fov, turretCam.fieldOfView, Mathf.Pow(0.5f, Time.deltaTime * zoomSpeed));
        currentLook.y = Mathf.Clamp(currentLook.y, -90f, 90f);

        rotor.localRotation = Quaternion.Euler(-currentLook.y, currentLook.x, 0f);
    }

    private void OnEnter()
    {
        if (!Loaded){
            AlertSystem.Message("Keine Pizza Geladen");
        }
        else {
            LaunchPizza();
        }
    }

    Queue<Transform> pizzas = new Queue<Transform>();
    Transform nextPizza => pizzas.TryDequeue(out Transform result) ? result : null;
    
    public bool Loaded => pizzas.TryPeek(out Transform x);

    public void LoadPizza(GameObject p) 
    {
        if (p.TryGetComponent(out Collider c)) c.enabled = false;
        if (p.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
        pizzas.Enqueue(p.transform);
    }

    private void LaunchPizza() 
    {
        Transform pizza = nextPizza;
        pizza.position = shootTransform.position;
        pizza.rotation = shootTransform.rotation;

        if (!pizza.TryGetComponent(out Rigidbody rb))
        {
            Debug.LogWarning("pizza has no Rigidbody attached?");
            return;
        }
        if (!pizza.TryGetComponent(out Collider c))
        {
            Debug.LogWarning("pizza has no collider attached");
            return;
        }
        c.enabled = true;
        rb.isKinematic = false;
        rb.velocity = pizza.forward * shootSpeed;
        rb.angularVelocity = Vector3.zero;
        Rigidbody carRB = rotor.parent.GetComponentInParent<Rigidbody>();
        if (carRB) rb.velocity += carRB.velocity;
    }
}
