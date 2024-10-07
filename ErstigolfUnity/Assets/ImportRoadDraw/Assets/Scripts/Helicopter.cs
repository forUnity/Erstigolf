using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform playerPos;
    public Transform spotlightTarget;
    public Transform spotlight;

    public Transform upperRotor;
    public Transform lowerRotor;

    public float rotorSpeed = 1000f;

    public float timeInSpotlight = 0;

    //public float allowedTimeInSpotlight = 10;

    public float movementSpeedFactor = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        AimSpotlight();
        MoveRotors();
        RotateTowardsPlayer();
        MoveTowardsPlayer();
    }

    private void MoveRotors() {
        upperRotor.Rotate(Vector3.up * rotorSpeed * Time.deltaTime);
        lowerRotor.Rotate(-Vector3.up * rotorSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer() {
        Vector3 playerAimSpot = playerPos.position;
        Vector3 playerDir = playerAimSpot - spotlightTarget.position;

        if (playerDir.magnitude > 2 && playerDir.magnitude < 20) playerDir = playerDir.normalized * 20;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(playerDir * movementSpeedFactor);
    }

    private void RotateTowardsPlayer() {
        Vector3 playerAimSpot = playerPos.position;
        playerAimSpot.y -= spotlightTarget.position.y;
        Vector3 playerDir = playerAimSpot - transform.position;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 torque = Vector3.Cross(transform.forward, playerDir) * 10;

        //only rotate around the y axis
        torque.x = 0;
        torque.z = 0;

        // limit torque to 10

        if (torque.magnitude > 100) {
            torque = torque.normalized * 100;
        }

        rb.AddTorque(torque);
    }

    private void AimSpotlight() {
        // check if player is close enough to spotlight target

        Quaternion currentSpotlightRotation = spotlight.rotation;
        Quaternion targetSpotlightRotation;

        if (Vector3.Distance(playerPos.position, spotlightTarget.position) < 12) {
            targetSpotlightRotation = Quaternion.LookRotation(playerPos.position - spotlight.position);
            timeInSpotlight += Time.deltaTime;
        } else {
            targetSpotlightRotation = Quaternion.LookRotation(spotlightTarget.position - spotlight.position);
            timeInSpotlight = 0;
        }

        spotlight.rotation = Quaternion.Slerp(currentSpotlightRotation, targetSpotlightRotation, Time.deltaTime * 2);

        //if (timeInSpotlight > allowedTimeInSpotlight) GameManager.instance.Loose();

        if(canDealDamage && timeInSpotlight > dmgAfterTime)
        {
            canDealDamage = false;
            PlayerCar.instance.Damage(dmgAmount);
            StartCoroutine(resetDamageCooldown());
        }
    }

    bool canDealDamage = true;
    [SerializeField] private float dmgCooldown = 0.2f;
    [SerializeField] private float dmgAmount = 0.05f;
    [SerializeField] private float dmgAfterTime = 0.8f;

    private IEnumerator resetDamageCooldown()
    {
        yield return new WaitForSeconds(dmgCooldown);
        canDealDamage = true;
    }
}
