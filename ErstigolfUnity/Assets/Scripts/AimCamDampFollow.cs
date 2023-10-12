using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamDampFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float LerpBase = 4;
    private void LateUpdate()
    {
        float lerpTime = Mathf.Pow(LerpBase, -Time.deltaTime);
        transform.position = target.position;
        //transform.position = Vector3.Lerp(transform.position, target.position, 1-lerpTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 1 - lerpTime);
    }
}
