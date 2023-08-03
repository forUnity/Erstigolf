using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private CarPlayerSteeringWheel steeringWheel;
    [Space]
    [SerializeField] private float driftFriction = 1.5f;
    //unused? [SerializeField] private float normalFriction = 0.2f;
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public List<GroundFriction> groundFrictions;
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public Transform centerOfMass;
    private bool isJumping = false;

    private int currentGround;

    public static PlayerCar instance;

    public float health = 0.5f;
    public float healingSpeed = 0.1f;

    public void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
        SetCurrentGround(0);

        instance = this;
    }
    
    
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    float upsidedownDuration;


    public void FixedUpdate()
    {
        UpdateUnderground();

        if (transform.up.y < 0){
            upsidedownDuration += Time.deltaTime;
        }
        else {
            upsidedownDuration = 0f;
        }
        if (upsidedownDuration >= 3f){
            transform.LookAt(transform.position + transform.forward, Vector3.up);
        }

        float motor = maxMotorTorque;// * Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) motor = -maxMotorTorque;
        float steering;
        if (!steeringWheel)
        {
            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        } 
        else
        {
            steering = steeringWheel.steeringWheelAxis * maxSteeringAngle;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) steering = 0f;
            
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        isJumping = Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > 0.1f;

        // add downforce to stabilize the car
        if (!isJumping) GetComponent<Rigidbody>().AddForce(-transform.up * 700 * GetComponent<Rigidbody>().velocity.magnitude);
        else GetComponent<Rigidbody>().AddForce(transform.up * 700);

        SetDrift( Input.GetKey(KeyCode.LeftShift));

        health = Mathf.Clamp01(health + healingSpeed * Time.deltaTime);
        HealthBar.Instance?.SetRemainingHealth(health);
    }

    public void SetCurrentGround(int index) {
        currentGround = index;
        foreach (AxleInfo axleInfo in axleInfos) {
            axleInfo.leftWheel.forwardFriction = FrictionCurveWithStiffness(
                axleInfo.leftWheel.forwardFriction, 
                groundFrictions[currentGround].forwardFriction);
            
            axleInfo.leftWheel.sidewaysFriction = FrictionCurveWithStiffness(
                axleInfo.leftWheel.sidewaysFriction, 
                groundFrictions[currentGround].sidewaysFriction);
            
            axleInfo.rightWheel.forwardFriction = FrictionCurveWithStiffness(
                axleInfo.rightWheel.forwardFriction, 
                groundFrictions[currentGround].forwardFriction);
            
            axleInfo.rightWheel.sidewaysFriction = FrictionCurveWithStiffness(
                axleInfo.rightWheel.sidewaysFriction, 
                groundFrictions[currentGround].sidewaysFriction);
        }

        GetComponent<Rigidbody>().drag = groundFrictions[currentGround].drag;
    }

    public void SetDrift(bool drift) {
        float sidewaysFriction;
        if (drift) sidewaysFriction = driftFriction;
        else sidewaysFriction = groundFrictions[currentGround].sidewaysFriction;
        foreach (AxleInfo axleInfo in axleInfos) {
            axleInfo.leftWheel.sidewaysFriction = FrictionCurveWithSlip(
                axleInfo.leftWheel.sidewaysFriction, 
                sidewaysFriction);
            
            axleInfo.rightWheel.sidewaysFriction = FrictionCurveWithSlip(
                axleInfo.rightWheel.sidewaysFriction, 
                sidewaysFriction);
        }
    }

    private WheelFrictionCurve FrictionCurveWithStiffness(WheelFrictionCurve curve, float stiffness) {
        WheelFrictionCurve wfc = curve;
        wfc.stiffness = stiffness;

        return wfc;
    }
    private WheelFrictionCurve FrictionCurveWithSlip(WheelFrictionCurve curve, float slip) {
        WheelFrictionCurve wfc = curve;
        wfc.extremumSlip = slip;
        return wfc;
    }

    public void UpdateUnderground() {
        bool allTiresOnRoad = true;
        bool allTiresInAir = true;
        // do raycast down at all 4 wheels
        foreach (AxleInfo axleInfo in axleInfos) {
            DoWheelRaycast(axleInfo.leftWheel.transform.position, out bool onRoadLeft, out bool inAirLeft);
            DoWheelRaycast(axleInfo.rightWheel.transform.position, out bool onRoadRight, out bool inAirRight);

            if (!onRoadLeft || !onRoadRight) allTiresOnRoad = false;
            if (!inAirLeft || !inAirRight) allTiresInAir = false;
        }

        if (allTiresOnRoad || allTiresInAir) {
            SetCurrentGround(0);
        } else {
            SetCurrentGround(1);
        }
    }

    private void DoWheelRaycast(Vector3 from, out bool onRoad, out bool inAir) {
        RaycastHit hit;
        bool didHit = Physics.Raycast(from, -transform.up, out hit, 0.7f, ~LayerMask.GetMask("Ignore Raycast"));
        
        if (didHit) {
            inAir = false;

            if (hit.collider.gameObject.tag == "Road") onRoad = true;
            else onRoad = false;
        }
        else
        {
            inAir = true;
            onRoad = false;
        }
    }

    public void Damage(float damage) {
        health -= damage;
        if (health <= 0) {
            GameManager.instance.Loose();
            // TODO: total sketchy, besser machen!!!
            healingSpeed = 0;
        }
    }
}
    
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    [Tooltip("is this wheel attached to motor?")]
    public bool motor;
    [Tooltip("does this wheel apply steer angle?")]
    public bool steering;
}

[System.Serializable]
public class GroundFriction {
    public string name;
    public float forwardFriction;
    public float sidewaysFriction;
    public float drag;
}