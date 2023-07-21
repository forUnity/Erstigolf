using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaThrower : MonoBehaviour
{
    // TODO: assign with PizzaMaker or sth like that
    public GameObject pizza;

    [SerializeField] private float scalePeriod = 1;
    [SerializeField] private float minScale = 1;
    [SerializeField] private float maxScale = 10;
    [SerializeField] private float maxAngle = 60;
    [SerializeField] private float shootSpeed = 20;
    [SerializeField] private Transform scaler;
    [SerializeField] private Transform rotor;
    [SerializeField] private Transform shootTransform;

    private AimState currentState;
    private float time;


    private void Update() {
        MeshRenderer r = rotor.GetComponentInChildren<MeshRenderer>();
        if (r) r.enabled = currentState != AimState.idle && currentState != AimState.ready;

        time += Time.deltaTime;
        float t = Mathf.Sin(time * 2 * Mathf.PI/scalePeriod);
        switch (currentState){
            case AimState.idle:
                if (pizza != null) {
                    currentState = AimState.ready;
                }
            break;
            case AimState.ready:
                // nothing
            break;
            case AimState.leftRight:
                float angle = maxAngle * t;
                rotor.localRotation = Quaternion.Euler(0, angle, 0);
            break;
            case AimState.upDown:
                float scale = Mathf.Lerp(minScale, maxScale, (t+1)/2);
                this.scaler.localScale = new Vector3(1,1, scale);
            break;
        }

        if (Input.GetKeyDown(KeyCode.Return)){
            OnEnter();
        }
    }

    private void OnEnter(){
        switch (currentState){
            case AimState.idle:
                // TODO: player feedback
                Debug.LogWarning("no pizza loaded");
            break;
            case AimState.ready:
                currentState = AimState.leftRight;
                time = 0;
                scaler.localScale = new Vector3(1,1,Mathf.Max(1, maxScale));
                rotor.localRotation = Quaternion.identity;
            break;
            case AimState.leftRight:
                currentState = AimState.upDown;
                time = 0;
            break;
            case AimState.upDown:
                LaunchPizza();
            break;
        }
    }

    private void LaunchPizza(){
        pizza.transform.position = shootTransform.transform.position;
        pizza.transform.rotation = shootTransform.transform.rotation;

        Rigidbody rb = pizza.GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogWarning("pizza has no Rigidbody attached?");
            return;
        }
        rb.velocity = pizza.transform.forward * shootSpeed * scaler.localScale.z;
        rb.angularVelocity = Vector3.zero;
        pizza = null;
        currentState = AimState.idle;
    }

    private enum AimState{
        idle, 
        ready,
        upDown,
        leftRight,
    }
}
