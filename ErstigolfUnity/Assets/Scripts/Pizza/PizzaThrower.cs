using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaThrower : MonoBehaviour
{
    [SerializeField] private float scalePeriod = 1;
    [SerializeField] private float minScale = 1;
    [SerializeField] private float maxScale = 10;
    [SerializeField] private float maxAngle = 60;
    [SerializeField] private float shootSpeed = 20;

    [SerializeField] private Transform scaler;
    [SerializeField] private Transform rotor;
    [SerializeField] private Transform shootTransform;

    private AimState currentState;

    private ButtonsInput inputs;
    private void Awake() {
        inputs = new ButtonsInput();
        inputs.Car.Green.performed += x => OnEnter();
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
        MeshRenderer r = rotor.GetComponentInChildren<MeshRenderer>();
        if (r) r.enabled = currentState != AimState.idle && currentState != AimState.ready;

        float amp = Mathf.Sin(Time.time * 2 * Mathf.PI / scalePeriod);
        if (currentState == AimState.leftRight)
        {
            rotor.localRotation = Quaternion.Euler(0, maxAngle * amp, 0);
        }
        if (currentState == AimState.upDown)
        {
            scaler.localScale = new Vector3(1, 1, Mathf.Lerp(minScale, maxScale, (amp + 1) / 2));
        }
    }

    private void OnEnter()
    {
        switch (currentState) 
        {
        case AimState.idle:
            AlertSystem.Message("Keine Pizza Geladen");
            break;
        case AimState.ready:
            currentState = AimState.leftRight;
            scaler.localScale = new Vector3(1,1,Mathf.Max(1, maxScale));
            rotor.localRotation = Quaternion.identity;
            break;
        case AimState.leftRight:
            currentState = AimState.upDown;
            break;
        case AimState.upDown:
            LaunchPizza();
            break;
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
        currentState = AimState.ready;
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
        rb.velocity = pizza.forward * shootSpeed * scaler.localScale.z;
        rb.angularVelocity = Vector3.zero;
        Rigidbody carRB = rotor.parent.GetComponentInParent<Rigidbody>();
        if (carRB) rb.velocity += carRB.velocity;
        currentState = Loaded ? AimState.ready : AimState.idle;
    }

    private enum AimState
    {
        idle, 
        ready,
        upDown,
        leftRight,
    }
}
