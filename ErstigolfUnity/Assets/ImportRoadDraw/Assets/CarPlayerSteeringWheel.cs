using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerSteeringWheel : MonoBehaviour
{
    //use for input. is clamped between -axisMax and axisMax
    public float steeringWheelAxis { get; private set; }
    [Header("Input")]
    private float axisMax = 1f;

    [SerializeField] private float changeAxisPerSecond = 0.5f;
    [SerializeField] private float snapBackperSecond = 0.7f;
    [Header("Visualize")]
    [SerializeField] private Transform steeringWheelGfxT;
    [SerializeField] private float maxRotationDeg;
    private float startRot;
    private void Start()
    {
        startRot = steeringWheelGfxT.eulerAngles.z;
    }
    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        steeringWheelAxis += inputX * changeAxisPerSecond * Time.deltaTime;
        if (steeringWheelAxis > 0f && inputX <= 0f)
        {
            if(Mathf.Abs(steeringWheelAxis) <= snapBackperSecond * Time.deltaTime)
            {
                steeringWheelAxis = 0f;
            } else
            {
                steeringWheelAxis -= snapBackperSecond * Time.deltaTime;
            }
        }
        if (steeringWheelAxis < 0f && inputX >= 0f)
        {
            if (Mathf.Abs(steeringWheelAxis) <= snapBackperSecond * Time.deltaTime)
            {
                steeringWheelAxis = 0f;
            }
            else
            {
                steeringWheelAxis += snapBackperSecond * Time.deltaTime;
            }
        }
        steeringWheelAxis = Mathf.Clamp(steeringWheelAxis, -axisMax, axisMax);

 
        //visualize
        steeringWheelGfxT.rotation = Quaternion.Euler(steeringWheelGfxT.eulerAngles.x, steeringWheelGfxT.eulerAngles.y, startRot - steeringWheelAxis /* / axisMax */ * maxRotationDeg);
    }
}
