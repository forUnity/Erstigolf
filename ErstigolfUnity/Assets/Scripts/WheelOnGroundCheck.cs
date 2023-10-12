using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOnGroundCheck : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private WheelCollider wheelCollider;
    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        wheelCollider = GetComponentInParent<WheelCollider>();
    }
    private void Update()
    {
        trailRenderer.emitting = wheelCollider.isGrounded;
    }
}

